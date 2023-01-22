using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamager : MonoBehaviour
{

    private GameObject collisionObject;

    public float damageAmount = 1;

    // continue to damage the colliding object
    void Update()
    {
        if(collisionObject == null) return;

        Damageable damageable = collisionObject.GetComponent<Damageable>();

        if(damageable != null){
            damageable.TakeDamage(damageAmount);

            // this object killed the collisionObject with the last damage call
            if(collisionObject == null) return;

            // objects with rigitbodies are pushed away
            Rigidbody2D collisionBody = collisionObject.GetComponent<Rigidbody2D>();
            if(collisionBody != null){
                
                Vector3 direction = collisionObject.transform.position - transform.position;
                collisionBody.velocity += (new Vector2(direction.x, direction.y)) * repulsionSpeed * 10 * Time.deltaTime;
            }

            
        }
            
        
    }

    public float repulsionSpeed = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionObject = collision.gameObject;
    }

    void OnCollisionExit2D(Collision2D collision)
    {  
        collisionObject = null;
    }
}
