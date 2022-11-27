using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject owner;
    Rigidbody2D bulletBody;
    static int id = 0;
    public int bulletId;
    int maximum_bounces = 3;
    static float bulletSpeed = 20f;
    [SerializeField] int bounces = 0;
    Vector2 lastVelocity;

    private void Start()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        bulletId = id++;
    }
    // Update is called once per frame
    void Update()
    {
        lastVelocity = bulletBody.velocity;
        if (bounces >= maximum_bounces)
        {//On maximum bounces the bullet shall be removed
            bounces = 0;
            SendMessageUpwards("DeactivateBullet", bulletId);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Bullet>() == null)
        {//Collision with everything except another Bullet, increases Bounces
            ++bounces;
        }
        if (collision.gameObject.GetComponent<Wall>() != null)
        {//Collision with a wall shall reflect the bullet accordingly
            bulletBody.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
        }
        Actor col = collision.gameObject.GetComponent<Actor>();
        if (col != null)
        {//Collision with an actor shall inflict damage
            if(col != owner.GetComponent<Actor>())
            {//Only inflicts damage, if not own bullets
                col.TakeDamage(owner.GetComponent<Actor>().DealDamage());
            }
        }
    }

    public void Shoot()
    {//Set initial velocity of bullet when spawned
        bulletBody.velocity = bulletBody.transform.up * bulletSpeed;
    }
}
