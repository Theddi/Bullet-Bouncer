using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        // rope should only interact with walls
        Physics2D.IgnoreLayerCollision(0, 6);
        Physics2D.IgnoreLayerCollision(1, 6);
        Physics2D.IgnoreLayerCollision(2, 6);
        Physics2D.IgnoreLayerCollision(3, 6);
        Physics2D.IgnoreLayerCollision(4, 6);
    }

    public float ropeSpeed;

    public float maxRopeLength;

    public Vector3 direction = Vector3.zero;

    // used for moving the user of the rope
    public Rigidbody2D userBody;

    public float ropeAngle = 0.0f;

    public float offsetAngle = 0.0f;

    public Vector3 offset = Vector3.zero;

    public bool isHooked = false;

    public Vector3 hookPoint = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // termporarly disable collision for transformation phase
        GetComponent<Collider2D>().enabled = false;

        if(!isHooked) Extend(ropeSpeed);
        else {
            Approach(hookPoint, ropeSpeed);

            // reset position and rotation
            transform.position = userBody.transform.position; 
            transform.rotation = Quaternion.identity; 

            //recalculate direction, scale, rotation and offset
            direction = hookPoint - userBody.transform.position;

            var length = direction.magnitude;
            direction.Normalize();

            transform.localScale = new Vector2(length,transform.localScale.y);

            ropeAngle = -Vector2.Angle(Vector2.right, direction);
            ropeAngle *= direction.y < 0 ? 1 : -1;
            transform.Rotate(0,0,ropeAngle);

            
            offset = new Vector3(length/2, 0.0f, 0.0f);
            // rotate around the z-axis
            offset = Quaternion.AngleAxis(-ropeAngle+1, Vector3.back) * offset;

            transform.position += offset;
        }

        // makes it look a bit better: the rope is attached to the upper body
        transform.position += Vector3.up * 0.5f;

        
        // reenable disable collision for transformation phase
        GetComponent<Collider2D>().enabled = true;
    }

    public void Offset()
    {
        var length = transform.localScale.x;

        offset = new Vector3(length/2, 0.0f, 0.0f);
        offsetAngle = Vector2.Angle(Vector2.right, direction);
        offsetAngle *= direction.y < 0 ? 1 : -1;
        // don't ask me why this is necessary
        offsetAngle += 30; 
        // rotate around the z-axis
        offset = Quaternion.AngleAxis(offsetAngle, Vector3.back) * offset;

        transform.position += offset;
    }

    public void Extend(float amount)
    {
        amount = amount * Time.deltaTime;

        // reset position to calculate everything anew
        transform.position = userBody.transform.position;

        // check length restriction
        if(transform.localScale.x + amount < maxRopeLength) {
            // increase size 
            transform.localScale += new Vector3(amount,0,0);
        }


        // offset correctly
        Offset();

        
        /* btw: the reset and Offset() have to be called every frame in the extending phase
         * because otherwise, the rope will fly off for some weird reason.
        */
    }

    public void Approach(Vector3 point, float speed)
    {
        userBody.velocity += (new Vector2(direction.x, direction.y)) * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        // ignore any further collison once hooked
        if(isHooked) return;
        
        if(collision.gameObject.tag == "Wall"){
            isHooked = true;
            hookPoint = collision.GetContact(0).point;

            // destroy the rope if a wall if piercing through the middle of the rope
            float distanceToHookPoint = (hookPoint - userBody.transform.position).magnitude;
            var length = transform.localScale.x;
            if(distanceToHookPoint * 1.05f < length) {
                GameObject.Destroy(gameObject); 
            }
        }
    }
}
