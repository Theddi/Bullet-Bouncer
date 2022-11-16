using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class Actor : MonoBehaviour
{
    Rigidbody2D body;
    PlayerControls controls;
    PlayerInput input;

    // direction the player canon faces
    [SerializeField] Vector2 currentShotFaceDirection = Vector2.zero;

    // where the last dpad movement was pointing to
    [SerializeField] Vector2 currentRopeFaceDirection = Vector2.zero;
    // direction the player will shoot the rope at
    [SerializeField] Vector2 currentRopeShotDirection = Vector2.zero;

    // flag whether the rope is currently out
    bool ropeShot;

    // how far the rope can be extended to reach objects
    [SerializeField] float ropeRange = 50.0f;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angle = 0f;
    BulletPool bulletPool;

    void Awake()
    {
        controls = new PlayerControls();
        input = GetComponent<PlayerInput>();

        controls.Gameplay.RopeShoot.performed += ctx => ShootRope();
        controls.Gameplay.RopeShoot.canceled += ctx => CancelRope();

        // store the direction to move in when shooting rope next time
        controls.Gameplay.Move.performed += ctx => currentRopeFaceDirection = ctx.ReadValue<Vector2>();
        
        // no direction to move in when no direction is pressed
        controls.Gameplay.Move.canceled += ctx => currentRopeFaceDirection = Vector2.zero;

        // store the rotation the canon should face
        controls.Gameplay.Rotation.performed += ctx => currentShotFaceDirection = ctx.ReadValue<Vector2>();

        controls.Gameplay.BulletShoot.performed += ctx => bulletPool.shooting_active = true;
        controls.Gameplay.BulletShoot.canceled += ctx => bulletPool.shooting_active = false;

    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        bulletPool = GameObject.Find("Bullet_Pool").GetComponent<BulletPool>();
    }

    List<GameObject> lines = new List<GameObject>();

// Update is called once per frame
    void Update()
    {
        // clear all the lines from the screen to draw new ones
        foreach(GameObject line in lines){
            GameObject.Destroy(line);
        }
        lines.Clear();

        //HandleInput();
        HandleMovement();
        HandleRotation();
    }

/*
// this was replaced by the handlers in the 'Awake()'
//If the new approach proves to be laggy, we could use this again
    void HandleInput()
    {
        if (controls.Gameplay.BulletShoot.IsPressed())
        {
            bulletPool.shooting_active = true;
        } else
        {
            bulletPool.shooting_active = false;
        }


        
    }

*/

    void HandleMovement()
    {
        // execute the rope movement
        if(ropeShot){
            // move in the direction stored when the fire button was pressed
            body.velocity += currentRopeShotDirection * speed * Time.deltaTime;

            // draw the rope
            if(hitPoint != Vector2.zero)
                DrawLine(transform.position, hitPoint, new Color(92.0f/255.0f, 47.0f/255.0f, 16.0f/255.0f), 0.2f);
            
            
        }

        // draw a hint line for where the player faces right now and what the player would hit with a rope shot
        if(currentRopeFaceDirection != Vector2.zero && !ropeShot){
            RaycastHit2D hit = PlayerRayCast(transform.position, currentRopeFaceDirection);

            // the line changes depending on wether the player will hit an obstacle
            if(hit.collider == null){
                Vector3 lineEndPoint = transform.position + 
                                        new Vector3(currentRopeFaceDirection.x, currentRopeFaceDirection.y, 0) * ropeRange;
                DrawLine(transform.position, lineEndPoint, Color.red, 0.04f);
            }else{
                DrawLine(transform.position, hit.point, Color.green, 0.04f);
            }
            
        }   
    }

    void HandleRotation()
    {
        if(currentShotFaceDirection.sqrMagnitude > 0.02)
        {
            angle = Vector2.Angle(Vector2.up, currentShotFaceDirection);
            angle *= currentShotFaceDirection.x < 0 ? 1 : -1;
            body.transform.eulerAngles = new Vector3(0, 0, angle);
        } else {
            body.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }


    Vector2 hitPoint = Vector2.zero;
    void ShootRope(){
        if(currentRopeFaceDirection != Vector2.zero && ropeShot == false){
            

            // store where to move as long as the fire button stays pressed
            currentRopeFaceDirection.Normalize();
            currentRopeShotDirection = currentRopeFaceDirection;
            
            // shot a raycast in the direction the player wants to move in
            RaycastHit2D hit = PlayerRayCast(transform.position, currentRopeShotDirection);

            //Debug.Log(hit.collider);

            // if it hits something...
            if (hit.collider != null)
            {
                ropeShot = true;

                // stop whatever movement was happening
                body.gravityScale = 0.0f;
                body.velocity = Vector2.zero;

                hitPoint = hit.point;

            // do not shoot rope if there is nothing hit
            }else{
                CancelRope();
            }
        }
    }

    void CancelRope(){
        ropeShot = false;
        hitPoint = Vector2.zero; 
        currentRopeShotDirection = Vector2.zero;
        body.gravityScale = 1.0f;
    }



//UTILS

    RaycastHit2D PlayerRayCast(Vector3 currentPlayerPosition, Vector2 direction){
        // the player should not hit itself with the raycast
        Vector2 startingPosition = new Vector2(currentPlayerPosition.x + direction.x * (GetComponent<CircleCollider2D>().radius*2 + 0.1f), 
                                                    currentPlayerPosition.y + direction.y * (GetComponent<CircleCollider2D>().radius*2 + 0.1f));
        // ~0 is a shortform for all 1's and in this case means all layers are selected
        // the depth is added to avoid bullets
        return Physics2D.Raycast(startingPosition, direction, ropeRange, ~0,  -0.05f);
    }


    void DrawLine(Vector3 start, Vector3 end, Color color, float width)
    {
        GameObject newLine = new GameObject();
        newLine.name = "Line";
        newLine.transform.position = start;
        newLine.AddComponent<LineRenderer>();
        LineRenderer renderer = newLine.GetComponent<LineRenderer>();
        renderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        renderer.startColor = color;
        renderer.endColor = color;
        renderer.startWidth = width;
        renderer.SetPosition(0, start);
        renderer.SetPosition(1, end);
        lines.Add(newLine);
    }
}
