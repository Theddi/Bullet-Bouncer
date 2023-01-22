using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager manager;
	public GameObject owner;
    public bool isPlayerBullet = false;

    Rigidbody2D bulletBody;
    static int id = 0;
    public int bulletId;
    int maximumBounces = 3;
    static float bulletSpeed = 20f;
    [SerializeField] int bounces = 0;
    Vector2 lastVelocity;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public float volumeScale;


	public void OnEnable()
    {
		bulletBody = GetComponent<Rigidbody2D>();
        if (owner != null ) {
			Physics2D.IgnoreCollision(GetComponent<Collider2D>(), owner.GetComponent<Collider2D>());
		}
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
            if(owner != collision.gameObject)
            {
				++bounces;
			}
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
            if (isPlayerBullet)
            {
				bulletBody.velocity = bulletBody.transform.up * bulletSpeed;
			} else
            {
				bulletBody.velocity = bulletBody.transform.right * bulletSpeed;
			}
        }
        if (audioSource != null)
			audioSource.PlayOneShot(audioClip, volumeScale);

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
