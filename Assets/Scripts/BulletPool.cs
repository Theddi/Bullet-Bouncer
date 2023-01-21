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
    int initialBulletCount = 25;
    int bullePoolIncrement = 5;
    [SerializeField] float cooldown = .5f;
    float timeToNextSpawn;
    [SerializeField] public bool shootingActive = false;

	private void Awake()
	{
		// initialize object pool by setting a capacity and initializing deactivated gameObjects
		// Note, that this results in a higher loading time at the beginning, since all objects need to be created
		bullePool = new List<GameObject>(initialBulletCount);
		for (int i = 0; i < initialBulletCount; i++)
		{
			bullePool.Add(createNewObject());
		}
	}

	void Start()
    {
        timeToNextSpawn = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextSpawn -= Time.deltaTime;
        if (shootingActive) //Spawns a bullet when cannon is in shooting mode
        {
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
		bt.GetComponent<Bullet>().owner = owner;
        bt.GetComponent<Bullet>().isPlayerBullet = (owner.GetComponent<Player>() != null);
		Physics2D.IgnoreCollision(bt.GetComponent<Collider2D>(), owner.GetComponent<Collider2D>());
		InititBulletForOwner(bt);
		bt.SetActive(false);
        return bt;
    }
    void InititBulletForOwner(GameObject bt)
    {// Initiates the bullet, depending on the owner of the bullet
        if (owner.GetComponent<Turret>())
        {
            bt.GetComponent<Bullet>().setMaximumBounces(1);
        } else if(!owner.GetComponent<Player>())
		{//Make big, because visible effect for undefined bullet
			bt.transform.localScale = new Vector3(2f, 2f, 1f); 
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

    public void DeactivateBullet(int bullet_id)
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
