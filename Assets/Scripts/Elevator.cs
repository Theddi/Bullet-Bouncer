using UnityEngine;

public class Elevator : MonoBehaviour
{

    public float distance;

    public Vector3 moveDirection = Vector3.up;


    public float speed = 0.5f;

    public bool onlyMoveOnce = false;

    public float cooldown = 1;

    public float assistanceSpeed = 500;

    private Vector3 targetPoint;
    private Vector3 startPoint;

    private bool active = false;

    private float timeUntilRestart = 0;

    private bool playerLost = false;


    // Start is called before the first frame update
    void Start()
    {
        targetPoint = transform.position + moveDirection* distance;
        startPoint = transform.position;

        Collider2D collider = GetComponent<Collider2D>();

        if(collider == null) Debug.LogError(gameObject + " is missing a Collider2D!");
    }

    private bool PlayerOnTop(){

        // only trigger if the player is ontop of the elevator
        Collider2D collider = GetComponent<Collider2D>();

        if(collider == null){
            Debug.LogError(gameObject + " is missing a Collider2D!");
            return false;
        } 

        float radiusY = (collider.bounds.size.y) / 2;
        float colliderYPos = transform.position.y + collider.offset.y;

        float radiusX = (collider.bounds.size.x) / 2;
        float colliderXPos = transform.position.x + collider.offset.x;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;

        Collider2D playerCollider = player.GetComponent<Collider2D>();

        float playerRadiusX = playerCollider.bounds.size.x/2;

        float playerDistanceY = player.transform.position.y - colliderYPos;

        // player touching from the top (also: NOT from below)...
        if(playerPos.y > colliderYPos + radiusY
            // and from the side... 
            && playerPos.x + playerRadiusX > colliderXPos - radiusX
            && playerPos.x - playerRadiusX < colliderXPos + radiusX
            && playerDistanceY < collider.bounds.size.y + playerCollider.bounds.size.y){

            // finally determined: player is indeed ontop of the elevator
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(active && targetPoint != null){

            // make sure that this will work correctly
            Collider2D collider = GetComponent<Collider2D>();
            if(collider == null) return;

            if(PlayerOnTop()) playerLost = false;

            Vector3 direction = targetPoint - startPoint;

            transform.position += direction * speed * Time.deltaTime;

            bool change;

            // determine whether the elevator should turn back the other way
            float distanceMoved = (transform.position - startPoint).magnitude;
            if(distanceMoved > distance) change = true;
            else change = false;

            // flipp the direction if the elevator get back down
            if(change){

                // set new target
                Vector3 temp = targetPoint;
                targetPoint = startPoint;
                startPoint = temp;

                transform.position = startPoint;

                if(onlyMoveOnce){
                    if(!playerLost) active = false;
                    else { 
                        playerLost = false;
                        lastTimePlayerLost = true;
                    }
                }else active = false;
                
                timeUntilRestart = Time.time + cooldown;
                moved = true;
                return;
            }

            // push the player to the middle of the elevator in order to make staying ontop easier
            if(!PlayerOnTop()) return;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
            Collider2D playerCollider = player.GetComponent<Collider2D>();

            Vector2 elevatorPos = new Vector2(transform.position.x, transform.position.y) + collider.offset;
            elevatorPos.Set(elevatorPos.x, elevatorPos.y + collider.bounds.size.y + playerCollider.bounds.size.y/2);

            Vector3 pushDirection = elevatorPos - new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 pushDirection2D = new Vector2(pushDirection.x, 0);

            playerBody.velocity = pushDirection2D * assistanceSpeed * Time.deltaTime;
        }


    }

    private bool moved = false;

    private bool lastTimePlayerLost = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ignore everything after the elevator moved once (exept if the player was lost in the process)
        if(onlyMoveOnce && moved && !lastTimePlayerLost) return;

        // ignore everything until cooldown is over
        bool onCooldown = timeUntilRestart != 0 && Time.time < timeUntilRestart;
        if(onCooldown) return;

        GameObject collisionObject = collision.gameObject;
        
        if(collisionObject.tag == "Player"){
 
            if(PlayerOnTop()) {
                active = true;
                lastTimePlayerLost = false;
                playerLost = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if(onlyMoveOnce && collision.gameObject.tag == "Player" && active) {
            playerLost = true;
        }
    }
}
