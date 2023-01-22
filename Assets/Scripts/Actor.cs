using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
	//Game Management
	protected GameManager manager;

	//Movement and Interaction
	protected Rigidbody2D body;
    [SerializeField] protected Vector2 moveDirection;
    [SerializeField] protected Vector2 rotation;
    [SerializeField] protected float speed;

    public void OnEnable(){
        manager = FindObjectOfType<GameManager>();
    }
    
    protected abstract void HandleMovement();

    protected abstract void HandleRotation();
}
