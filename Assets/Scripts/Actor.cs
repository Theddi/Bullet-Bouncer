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

    //Stats
    [SerializeField] protected float health;
    [SerializeField] protected float baseDamage;
    [SerializeField] protected float damageMulti;
    [SerializeField] protected float damageReduct;

    public void TakeDamage(float damage)
    {
        //Debug.Log("took damage");
        this.health -= damage * damageReduct;
        if(health <= 0)
        {
            HandleDeath();
		}
    }

    public float DealDamage()
    {
        //Debug.Log("dealt damage");
        return baseDamage * damageMulti;
    }
    protected void InitiateActor(float health, float damage, float multi, float reduct)
    {
        this.health = health;
        this.damageReduct = reduct;
        this.damageMulti = multi;
        this.baseDamage = damage;
		manager = FindObjectOfType<GameManager>();
	}
    protected abstract void HandleMovement();

    protected abstract void HandleRotation();

    protected abstract void HandleDeath();
}
