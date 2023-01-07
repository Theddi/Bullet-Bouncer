using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager manager;
	public GameObject owner;
    bool isPlayerBullet = false;

    Rigidbody2D bulletBody;
    static int id = 0;
    public int bulletId;
    int maximumBounces = 3;
    static float bulletSpeed = 20f;
    [SerializeField] int bounces = 0;
    Vector2 lastVelocity;

    private void Start()
    {
		manager = FindObjectOfType<GameManager>();
		bulletBody = GetComponent<Rigidbody2D>();
        bulletId = id++;
        isPlayerBullet = (owner.GetComponent<Player>() != null);
	}
    // Update is called once per frame
    void Update()
    {
        lastVelocity = bulletBody.velocity;
        if (bounces >= maximumBounces)
        {//On maximum bounces the bullet shall be removed
			if(isPlayerBullet)
			{
				manager.IncreaseScore(1);
			}
			DestroyBullet();
		}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bulletCollision = collision.gameObject.GetComponent<Bullet>();
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
        Actor col = collision.gameObject.GetComponent<Actor>();
        if (col != null)
        {//Collision with an actor shall inflict damage, and remove that bullet
            if(col != owner.GetComponent<Actor>())
            {//Only inflicts damage, if not own bullets
                col.TakeDamage(owner.GetComponent<Actor>().DealDamage());
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
        bulletBody.velocity = bulletBody.transform.up * bulletSpeed;
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
