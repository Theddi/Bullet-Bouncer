using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bullet_body;
    static int id = 0;
    public int bullet_id;
    [SerializeField] int maximum_bounces = 3;
    static float bullet_speed = 20f;
    [SerializeField] int bounces = 0;
    Vector2 last_velocity;

    private void Start()
    {
        bullet_body = GetComponent<Rigidbody2D>();
        bullet_id = id++;
    }
    // Update is called once per frame
    void Update()
    {
        last_velocity = bullet_body.velocity;
        if (bounces >= maximum_bounces)
        {//On maximum bounces the bullet shall be removed
            bounces = 0;
            SendMessageUpwards("DeactivateBullet", bullet_id);
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
            bullet_body.velocity = Vector2.Reflect(last_velocity, collision.contacts[0].normal);
        }
    }

    public void Shoot()
    {//Set initial velocity of bullet when spawned
        bullet_body.velocity = bullet_body.transform.up * bullet_speed;
    }
}
