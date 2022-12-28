using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bullet;
    public GameObject origin;
    public GameObject owner;
    List<GameObject> bullePool;
    [SerializeField] int activeBullets = 0;
    int initialBulletCount = 100;
    int bullePoolIncrement = 20;
    [SerializeField] float cooldown = .5f;
    float timeToNextSpawn;
    float lastDeltaTime;
    [SerializeField] public bool shootingActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // initialize object pool by setting a capacity and initializing deactivated gameObjects
        // Note, that this results in a higher loading time at the beginning, since all objects need to be created
        bullePool = new List<GameObject>(initialBulletCount);
        for (int i = 0; i < initialBulletCount; i++)
        {
            bullePool.Add(createNewObject());
        }
        // set the cooldown until the next spawn (if autoSpawn is activated)
        timeToNextSpawn = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        lastDeltaTime = Time.deltaTime;
        if (shootingActive) //Spawns a bullet when cannon is in shooting mode
        {
            timeToNextSpawn -= Time.deltaTime;
            if (timeToNextSpawn < 0)
            {
                SpawnBullet();
                timeToNextSpawn = cooldown;
            }
        }
    }

    GameObject createNewObject()
    {
        // create a new object, deactivate it and assign its parent transform to the spawner
        var bt = Instantiate<GameObject>(bullet);
        bt.transform.parent = transform;
        InititBulletForOwner(bt);
        bt.SetActive(false);
        bt.GetComponent<Bullet>().owner = this.owner;
        return bt;
    }
    void InititBulletForOwner(GameObject bt)
    {// Sets the scale for the bullet, depending on the owner of the bullet
        string type = this.owner.name;
        switch(type)
        {
            case "Player":
                bt.transform.localScale = new Vector3(.5f, .5f, 1f);
                bt.GetComponent<Rigidbody2D>().gravityScale = 2;
                break;
            case "Turret":
                bt.GetComponent<Bullet>().setMaximumBounces(1);
                bt.transform.localScale = new Vector3(.3f, .3f, 1f);
                break;
            default:
                bt.transform.localScale = new Vector3(2f, 2f, 1f); //Make big, because visible effect
                break;
        }
    }
    void IncreasePool()
    {
        bullePool.Capacity += bullePoolIncrement;
        for (int i = activeBullets; i < bullePool.Capacity; i++)
        {
            bullePool.Add(createNewObject());
        }
    }
    void SpawnBullet()
    {
        // in case all pool objects are already in use, resize the pool
        if (activeBullets == bullePool.Capacity)
            IncreasePool();

        // get the first deactivated object and activate it
        var currentBT = bullePool[activeBullets];
        currentBT.SetActive(true);
        
        // put the bullet slightly in the background so that raycasts won't detect it
        Vector3 spawnPos = new Vector3(origin.transform.position[0], origin.transform.position[1], -0.1f);
        currentBT.transform.SetPositionAndRotation(spawnPos, origin.transform.rotation);
        currentBT.GetComponent<Bullet>().Shoot();

        // increase active objects counter
        activeBullets += 1;
    }

    void DeactivateBullet(int bullet_id)
    {
        //Debug.Log("Deactivate");
        for(int index = 0; index < bullePool.Count; index++)
        {
            Bullet bt = bullePool[index].GetComponent<Bullet>();
            if (bt.bulletId == bullet_id && bullePool[index].activeSelf)
                DestroyObject(index);
        }
    }

    void DestroyObject(int destroy_index)
    {
        // deactivate the selected object immediately
        bullePool[destroy_index].SetActive(false);

        // swap with last activated object to keep the list sorted
        var tmp = bullePool[destroy_index];
        bullePool[destroy_index] = bullePool[activeBullets - 1];
        bullePool[activeBullets - 1] = tmp;

        // reduce active objects counter
        activeBullets -= 1;
    }
}
