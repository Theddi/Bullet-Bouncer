using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Actor
{
	Player target;
    Transform cannon; // Transform of the cannon (it's origin)
    BulletPool shoot;
	Vector2 direction = Vector2.zero;
    float targetRange = 1000f;

	// Start is called before the first frame update
	void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
		InitiateActor(10f, 1f, 1f, 1f);
        this.speed = 0f;
        cannon = transform.Find("Cannon_Rotary_Axis");
		shoot = transform.Find("bulletPool").GetComponent<BulletPool>();
	}

    // Update is called once per frame
    void Update()
    {
		direction = target.transform.position - cannon.position;
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
        cannon.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    protected override void HandleDeath()
    {
        manager.IncreaseScore(100);
		gameObject.SetActive(false);
		shoot.shootingActive = false;
		enabled = false;
	}
}
