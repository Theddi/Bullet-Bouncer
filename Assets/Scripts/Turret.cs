using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Actor
{
    Player target;
    GameObject cannon;
    BulletPool shoot;
	Vector2 direction = Vector2.zero;
    float targetRange = 1000f;

	// Start is called before the first frame update
	void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        initiateStats(1f, 1f, 1f, 1f);
        this.speed = 0f;
        cannon = GameObject.Find("Cannon_Rotary_Axis");
		shoot = GameObject.Find("Turret_Bullet_Pool").GetComponent<BulletPool>();

	}

    // Update is called once per frame
    void Update()
    {
		direction = target.transform.position - cannon.transform.position;
		HandleDeath();
        HandleMovement();
        if (direction.sqrMagnitude < targetRange)
        {
			shoot.shootingActive= true;
            HandleRotation();
        } else {
			shoot.shootingActive = false;
		}
    }

    protected override void HandleMovement()
    {

    }

    protected override void HandleRotation()
    {
        cannon.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    protected override void HandleDeath()
    {
    
    }
}
