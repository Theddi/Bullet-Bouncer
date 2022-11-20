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
    float health = 1f;
    float baseDamage = 1f;
    float damageMulti = 1f;
    float damageReduct = 1f;

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
