using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerApproacher : MonoBehaviour
{

    public float targetRange = 15;

    public float approachSpeed = 1;

    GameObject player;
    public AudioSource audioWhenApproaching;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
	}

    public static bool PlayerInSight(Transform transform, float targetRange){
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = new Vector2(player.transform.position.x, player.transform.position.y) - pos2D;

        // check direct sight with raycast
        RaycastHit2D[] allHits = Physics2D.RaycastAll(pos2D, direction, targetRange);
        foreach(RaycastHit2D hit in allHits){
            // ignore hitting itself
            if(hit.transform == transform) continue;

            // first hit is the player ? (aside from the object itself)
            if(hit.transform.tag == "Player"){

                return true;

            // someting is in the way
            } else return false;
            
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = (direction).magnitude;

        // approach the player if visible and in range
        if(distance > targetRange) return;

        
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        if(body == null){
            Debug.LogWarning(gameObject + " is missing a RigidBody2D!");
            return;
        }

        // only target when in sight (no obstacles in the way)
        if(PlayerInSight(transform, targetRange))
        {
			body.velocity += (new Vector2(direction.x, direction.y)) * approachSpeed * Time.deltaTime;
            if (audioWhenApproaching != null)
                audioWhenApproaching.mute = false;
		} else
        {
			if (audioWhenApproaching != null)
				audioWhenApproaching.mute = true;
		}
    }
}
