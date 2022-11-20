using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : Actor
{
    //controls
    PlayerControls controls;
    PlayerInput input;

    //Movement and Interaction
    Rigidbody2D body;
    [SerializeField] Vector2 moveDirection = Vector2.zero;
    [SerializeField] Vector2 rotation = Vector2.zero;
    [SerializeField] float speed = 10.0f;
    [SerializeField] float angle = 0f;

    //Stats
    float health = 100;
    float damageReduct = 1f;
    float baseDamage = 1f;
    float damageMulti = 1f;

    void Awake()
    {
        controls = new PlayerControls();
        input = GetComponent<PlayerInput>();
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
        
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    void HandleInput()
    {
        moveDirection = controls.Gameplay.Move.ReadValue<Vector2>();
        rotation = controls.Gameplay.Rotation.ReadValue<Vector2>();
    }

    public override void HandleMovement()
    {
        body.velocity = moveDirection * speed;
    }

    public override void HandleRotation()
    {
        if(rotation.sqrMagnitude > 0.02)
        {
            angle = Vector2.Angle(Vector2.up, rotation);
            angle *= rotation.x < 0 ? 1 : -1;
            body.transform.eulerAngles = new Vector3(0, 0, angle);
        } else {
            body.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    public override void HandleDeath()
    {
        if(health <= 0)
            Time.timeScale = 0f;
    }
}
