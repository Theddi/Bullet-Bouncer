using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerApproacher : MonoBehaviour
{

    public float targetRange = 15;

    public float approachSpeed = 1;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = (direction).magnitude;

        if(distance < targetRange){
            Rigidbody2D body = GetComponent<Rigidbody2D>();

            if(body == null){
                Debug.LogWarning(gameObject + " is missing a RigidBody2D!");
                return;
            }

            // todo check direct sight with raycast
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            RaycastHit2D[] allHits = Physics2D.RaycastAll(pos2D, direction, targetRange);
            foreach(RaycastHit2D hit in allHits){
                // ignore hitting itself
                if(hit.transform == transform) continue;

                // first hit is the player ? (aside from the object itself)
                if(hit.transform.tag == "Player"){

                    // only target when in sight (no obstacles in the way)
                    body.velocity += (new Vector2(direction.x, direction.y)) * approachSpeed * Time.deltaTime;
                    return;

                // someting is in the way
                } else return;
                
            }

            
        }
    }
}
