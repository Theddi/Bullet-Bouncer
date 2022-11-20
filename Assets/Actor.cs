using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    //Movement and Interaction
    Rigidbody2D body;
    Vector2 moveDirection;
    Vector2 rotation;
    float speed;

    //Stats
    float health;
    float baseDamage;
    float damageMulti;
    float damageReduct;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleDeath();
    }

    public void TakeDamage(float damage)
    {
        health -= damage * damageReduct;
    }

    public float DealDamage()
    {
        return baseDamage * damageMulti;
    }

    public abstract void HandleMovement();

    public abstract void HandleRotation();

    public abstract void HandleDeath();
}
