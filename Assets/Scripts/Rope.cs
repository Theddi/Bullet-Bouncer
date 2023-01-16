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

            Offset();
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
        // using right vector since the rope prefab is horizontal
        offsetAngle = Vector2.Angle(Vector2.right, direction);
        offsetAngle *= direction.y < 0 ? 1 : -1;
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
        
        if(collision.gameObject.tag == "Wall"){

            Vector3 tempHookPoint = collision.GetContact(0).point;

            // only set the hookpoint once
            if(!isHooked){
                isHooked = true;
                hookPoint = tempHookPoint;
            }

            // rope is broken because something pierced the middle
            float distanceToHookPoint = (tempHookPoint - userBody.transform.position).magnitude;
            var length = transform.localScale.x;
            if(distanceToHookPoint * 1.1f < length) GameObject.Destroy(gameObject); // 10% errorspan to make this filter more lenient
            
        }
    }
}
