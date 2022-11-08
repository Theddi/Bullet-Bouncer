using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bullet_body;
    static int id = 0;
    public int bullet_id;
    int maximum_bounces = 3;
    float bullet_speed = 20f;
    [SerializeField] int bounces = 0;

    private void Start()
    {
        bullet_body = GetComponent<Rigidbody2D>();
        bullet_id = id++;
    }
    // Update is called once per frame
    void Update()
    {
        bullet_body.transform.eulerAngles = new Vector3(bullet_body.transform.eulerAngles.x, bullet_body.transform.eulerAngles.y, 0);
        if (bounces >= maximum_bounces)
        {
            bounces = 0;
            SendMessageUpwards("DeactivateBullet", bullet_id);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Bullet>() == null)
        {
            ++bounces;
        }
        bullet_body.velocity = Vector2.Reflect(bullet_body.velocity, collision.contacts[0].normal);
    }
}
