using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{

    Rigidbody2D body;
    public float speed = 5.0f;

    Vector2 currentMousePosition = Vector2.zero;

    Camera cam;

    PlayerControls controls;
    Vector2 currentDpadDirection = Vector2.zero;
    Vector2 currentShotDirection = Vector2.zero;

    void Awake(){
        controls = new PlayerControls();

        controls.Gameplay.Shoot.performed += ctx => ShootRope();
        
        // enable normal movement
        controls.Gameplay.Shoot.canceled += ctx => CancelRope();

        // store the direction to move in when shooting next time
        controls.Gameplay.Move.performed += ctx =>currentDpadDirection = ctx.ReadValue<Vector2>();
        
        // no direction to move in when no direction is pressed
        controls.Gameplay.Move.canceled += ctx => currentDpadDirection = Vector2.zero;  
        
        
    }


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    bool shot = false;
    Vector2 hitPoint = Vector2.zero;
    void ShootRope(){
        if(currentDpadDirection != Vector2.zero && shot == false){
            

            // store where to move as long as the fire button stays pressed
            currentDpadDirection.Normalize();
            currentShotDirection = currentDpadDirection;
            
            // shot a raycast in the direction the player wants to move in
            RaycastHit2D hit = PlayerRayCast(transform.position, currentShotDirection);

            //Debug.Log(hit.collider);

            // if it hits something...
            if (hit.collider != null)
            {
                shot = true;

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
        shot = false;
        hitPoint = Vector2.zero; 
        currentShotDirection = Vector2.zero;
        body.gravityScale = 1.0f;
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

        if(shot){
            // move in the direction stored when the fire button was pressed
            body.velocity += currentShotDirection * speed * Time.deltaTime;

            // draw the rope
            if(hitPoint != Vector2.zero)
                DrawLine(transform.position, hitPoint, new Color(92.0f/255.0f, 47.0f/255.0f, 16.0f/255.0f), 0.1f);
            
            
        }

        // draw a hint line for where the player faces right now and what the player would hit with a rope shot
        if(currentDpadDirection != Vector2.zero && !shot){
            RaycastHit2D hit = PlayerRayCast(transform.position, currentDpadDirection);

            // the line changes depending on wether the player will hit an obstacle
            if(hit.collider == null){
                Vector3 lineEndPoint = transform.position + new Vector3(currentDpadDirection.x, currentDpadDirection.y, 0) * 100;
                DrawLine(transform.position, lineEndPoint, Color.red, 0.02f);
            }else{
                DrawLine(transform.position, hit.point, Color.green, 0.02f);
            }
            
        }
        
    }

//UTILS

    RaycastHit2D PlayerRayCast(Vector3 currentPlayerPosition, Vector2 direction){
        // the player should not hit itself with the raycast
        Vector2 startingPosition = new Vector2(currentPlayerPosition.x + direction.x * (GetComponent<CircleCollider2D>().radius + 0.1f), 
                                                    currentPlayerPosition.y + direction.y * (GetComponent<CircleCollider2D>().radius + 0.1f));
        return Physics2D.Raycast(startingPosition, direction);
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
