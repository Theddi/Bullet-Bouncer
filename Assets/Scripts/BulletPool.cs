using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bullet;
    public GameObject origin;
    public GameObject owner;
    List<GameObject> bullet_pool;
    [SerializeField] int active_bullets = 0;
    int initial_bullet_count = 100;
    int bullet_pool_increment = 20;
    [SerializeField] float cooldown = .5f;
    float time_to_next_spawn;
    float last_delta_time;
    [SerializeField] public bool shooting_active = false;

    // Start is called before the first frame update
    void Start()
    {
        // initialize object pool by setting a capacity and initializing deactivated gameObjects
        // Note, that this results in a higher loading time at the beginning, since all objects need to be created
        bullet_pool = new List<GameObject>(initial_bullet_count);
        for (int i = 0; i < initial_bullet_count; i++)
            bullet_pool.Add(createNewObject());

        // set the cooldown until the next spawn (if autoSpawn is activated)
        time_to_next_spawn = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        last_delta_time = Time.deltaTime;
        if (shooting_active) //Spawns a bullet when cannon is in shooting mode
        {
            time_to_next_spawn -= Time.deltaTime;
            if (time_to_next_spawn < 0)
            {
                SpawnBullet();
                time_to_next_spawn = cooldown;
            }
        }
    }

    GameObject createNewObject()
    {
        // create a new object, deactivate it and assign its parent transform to the spawner
        var bt = Instantiate<GameObject>(bullet);
        bt.transform.parent = transform;
        InitialBulletScale(bt);
        bt.SetActive(false);
        bt.GetComponent<Bullet>().owner = this.owner;
        return bt;
    }
    void InitialBulletScale(GameObject bt)
    {
        string type = this.owner.name;
        switch(type)
        {
            case "Player":
                bt.transform.localScale = new Vector3(.5f, .5f, 1f);
                break;
            case "Turret":
                bt.transform.localScale = new Vector3(.3f, .3f, 1f);
                break;
            default:
                bt.transform.localScale = new Vector3(2f, 2f, 1f); //Make big, because visible effect
                break;
        }
    }
    void IncreasePool()
    {
        bullet_pool.Capacity += bullet_pool_increment;
        for (int i = active_bullets; i < bullet_pool.Capacity; i++)
        {
            bullet_pool.Add(createNewObject());
        }
    }
    void SpawnBullet()
    {
        // in case all pool objects are already in use, resize the pool
        if (active_bullets == bullet_pool.Capacity)
            IncreasePool();

        // get the first deactivated object and activate it
        var currentBT = bullet_pool[active_bullets];
        currentBT.SetActive(true);
        
        // put the bullet slightly in the background so that raycasts won't detect it
        Vector3 spawnPos = new Vector3(origin.transform.position[0], origin.transform.position[1], -0.1f);
        currentBT.transform.SetPositionAndRotation(spawnPos, origin.transform.rotation);
        currentBT.GetComponent<Bullet>().Shoot();

        // increase active objects counter
        active_bullets += 1;
    }

    void DeactivateBullet(int bullet_id)
    {
        //Debug.Log("Deactivate");
        for(int index = 0; index < bullet_pool.Count; index++)
        {
            Bullet bt = bullet_pool[index].GetComponent<Bullet>();
            if (bt.bulletId == bullet_id && bullet_pool[index].activeSelf)
                DestroyObject(index);
        }
    }

    void DestroyObject(int destroy_index)
    {
        // deactivate the selected object immediately
        bullet_pool[destroy_index].SetActive(false);

        // swap with last activated object to keep the list sorted
        var tmp = bullet_pool[destroy_index];
        bullet_pool[destroy_index] = bullet_pool[active_bullets - 1];
        bullet_pool[active_bullets - 1] = tmp;

        // reduce active objects counter
        active_bullets -= 1;
    }
}
