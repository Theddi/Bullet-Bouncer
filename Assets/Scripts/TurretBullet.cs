using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    GameManager manager;
	public GameObject owner;
    public bool isPlayerBullet = false;
    public GameObject target;

    Rigidbody2D bulletBody;
    static int id = 0;
    public int bulletId;
    int maximumBounces = 3;
    static float bulletSpeed = 20f;
    [SerializeField] int bounces = 0;
    Vector2 lastVelocity;

    float turretX;
    float targetX;
    float dist;
    float nextX;
    float baseY;
    float height;

    public void OnEnable()
    {
		bulletBody = GetComponent<Rigidbody2D>();
		target = GameObject.Find("Player");
	}

    private void Start()
    {
		manager = FindObjectOfType<GameManager>();
		bulletId = id++;
		bulletBody = GetComponent<Rigidbody2D>();
	}
    // Update is called once per frame
    void Update()
    {
        if (bounces >= maximumBounces)
        {//On maximum bounces the bullet shall be removed
			if(isPlayerBullet)
			{
				manager.IncreaseScore(1);
			}
			DestroyBullet();
		}
        dist = targetX - turretX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, bulletSpeed * Time.deltaTime);
        baseY = Mathf.Lerp(owner.transform.position.y, target.transform.position.y, (nextX - turretX) / dist);
        height = 2 * (nextX - turretX) * (float)((nextX - targetX) / (-0.25 * dist * dist));

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;
    }

    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0,0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bulletCollision = collision.gameObject.GetComponent<Bullet>();
        //Debug.Log(collision.gameObject);
		if (bulletCollision == null)
        {//Collision with everything except another Bullet, increases Bounces
            ++bounces;
        } else {
            if(owner != bulletCollision.owner && isPlayerBullet)
            {//Increase score by 1 for a bullet collision, of different owners
				manager.IncreaseScore(1);
			}
			DestroyBullet();
		}

        if (collision.gameObject.GetComponent<Wall>() != null)
        {//Collision with a wall shall reflect the bullet accordingly
            bulletBody.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
        }
        //Collision with a damageable shall inflict damage, and remove that bullet
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            //Only inflicts damage to bullets, that are not ones own
            if(collision.gameObject.GetComponent<Actor>() != owner.GetComponent<Actor>())
            {
                damageable.TakeDamage(owner.GetComponent<Damageable>().DealDamage());
                if(isPlayerBullet)
                {//When the player hit another actor, increase score by 10
					manager.IncreaseScore(10);
				}
				DestroyBullet();
			}
        }
    }

    public void Shoot()
    {//Set initial velocity of bullet when spawned
        if (bulletBody)
        {
			turretX = transform.position.x;
			targetX = target.transform.position.x;
		}
            
    }

    public void setMaximumBounces(int max)
    {
		maximumBounces = max;   
    }

	public void DestroyBullet()
	{
        //Debug.Log("Deactivate on Line:"+line);
		bounces = 0;
        owner.transform.Find("bulletPool").GetComponent<BulletPool>().DeactivateBullet(bulletId);
	}
}
