using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : Actor
{
    PlayerControls controls;
    PlayerInput input;

    // Shoot interaction
    BulletPool bulletPool; // used for spawning bullets
    Vector2 currentShotFaceDirection = Vector2.zero; // direction the player canon faces
    float angle = 0f;

    // where the last dpad movement was pointing to
    public Vector2 currentRopeFaceDirection = Vector2.zero; 

    // direction the player will shoot the rope at
    public Vector2 currentRopeShotDirection = Vector2.zero; 
    // flag whether the rope is currently out
    bool ropeShot; 
    [SerializeField] float ropeRange = 50.0f; // how far the rope can be extended to reach objects
    
    void Awake()
    {
        controls = new PlayerControls();
        input = GetComponent<PlayerInput>();

        // Rope & Movement
        controls.Gameplay.RopeShoot.performed += ctx => ShootRope();
        controls.Gameplay.RopeShoot.canceled += ctx => CancelRope();
        controls.Gameplay.Move.performed += ctx => currentRopeFaceDirection = ctx.ReadValue<Vector2>(); // store the direction to move in when shooting rope next time
        controls.Gameplay.Move.canceled += ctx => currentRopeFaceDirection = Vector2.zero; // no direction to move in when no direction is pressed

        // shooting
        controls.Gameplay.Rotation.performed += ctx => currentShotFaceDirection = ctx.ReadValue<Vector2>(); // store the rotation the canon should face
        controls.Gameplay.BulletShoot.performed += ctx => bulletPool.shootingActive = true;
        controls.Gameplay.BulletShoot.canceled += ctx => bulletPool.shootingActive = false;

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
        bulletPool = GameObject.Find("Player_Bullet_Pool").GetComponent<BulletPool>();
        initiateStats(5f, 1f, 1f, 1f);
        this.speed = 5f;
    }


    public GameObject rope;
    public GameObject playerRope;

    List<GameObject> ropes = new List<GameObject>();

// Update is called once per frame
    void Update()
    {

        //  clear all unnecessary ropes
        // this removal approach ensures no ropes will be missed indefinitely 
        foreach(GameObject rope in ropes)
        {
            if(rope != playerRope)
                GameObject.Destroy(rope);
        }
        ropes.Clear();
        if(playerRope != null) ropes.Add(playerRope);



        // HandleInput();
        if(Time.timeScale > 0)
        {
            HandleMovement();
            HandleRotation();
            HandleDeath();
        }
    }
    protected override void HandleMovement()
    {
        // moved to Rope.Update(), since all the movement comes from the rope
    }

    protected override void HandleRotation()
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

    protected override void HandleDeath()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
        }
    }

    

    

    Vector2 hitPoint = Vector2.zero;
    void ShootRope()
    {
        // do not execute when the game is frozen
        if(Time.timeScale == 0) return;

        // this spawns a rope attached to the player that extends until it hits something or is extended to maximum length
        if(currentRopeFaceDirection != Vector2.zero && ropeShot == false)
        {
            ropeShot = true;

            // store where to move as long as the fire button stays pressed
            currentRopeFaceDirection.Normalize();
            currentRopeShotDirection = currentRopeFaceDirection;

            // create and init the rope
            var ropeAngle = Vector2.Angle(Vector2.up, currentRopeShotDirection);
            ropeAngle *= currentRopeShotDirection.x < 0 ? 1 : -1;

            // the rope faces in the pressed direction
            playerRope = Instantiate(rope);
            //playerRope.transform.SetParent(transform);
            playerRope.transform.position = transform.position;
            playerRope.transform.Rotate(0,0,ropeAngle);

            playerRope.GetComponent<Rope>().direction = new Vector3(currentRopeShotDirection.x, currentRopeShotDirection.y, 0);
            playerRope.GetComponent<Rope>().Offset();
            playerRope.GetComponent<Rope>().userBody = body;

            ropes.Add(playerRope);
        }
    }

    void CancelRope()
    {
        ropeShot = false;
        hitPoint = Vector2.zero; 
        currentRopeShotDirection = Vector2.zero;
        playerRope = null; // the rope is destroyed in Update()
    }

// UTILS

// CURRENTLY NOT USED, maybe used later

    [SerializeField] float raycastNoInterceptionRadius = 15f;

    RaycastHit2D PlayerRayCast(Vector3 currentPlayerPosition, Vector2 direction)
    {
        // the player should not hit itself with the raycast
        Vector2 startingPosition = new Vector2(currentPlayerPosition.x + direction.x * raycastNoInterceptionRadius, 
                                                    currentPlayerPosition.y + direction.y * raycastNoInterceptionRadius);
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
        //lines.Add(newLine);
    }
}
