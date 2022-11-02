using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class Actor : MonoBehaviour
{
    Rigidbody2D player_body;
    PlayerControls controls;
    PlayerInput input;
    Vector2 face_direction = Vector2.up;

    [SerializeField] Vector2 player_move = Vector2.zero;
    [SerializeField] Vector2 rotation = Vector2.zero;
    [SerializeField] float speed = 10.0f;
    [SerializeField] float angle = 0f;

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
        player_body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    void HandleInput()
    {
        player_move = controls.Gameplay.Move.ReadValue<Vector2>();
        rotation = controls.Gameplay.Rotation.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        player_body.velocity = player_move * speed;
    }

    void HandleRotation()
    {
        if(rotation.sqrMagnitude > 0.02)
        {
            angle = Vector2.Angle(Vector2.up, rotation);
            angle *= rotation.x < 0 ? 1 : -1;
            player_body.transform.eulerAngles = new Vector3(0, 0, angle);
        } else {
            player_body.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
