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
    float motionDegree = 125f;

	// Start is called before the first frame update
	void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        this.speed = 0f;
        cannon = transform.Find("Cannon_Rotary_Axis");
		shoot = transform.Find("bulletPool").GetComponent<BulletPool>();
        GetComponent<Damageable>().changeDeathFuntion(HandleDeath);
	}

    // Update is called once per frame
    void Update()
    {
		direction = target.transform.position - cannon.position;
        HandleMovement();
        if (direction.sqrMagnitude < targetRange)
        {
            HandleRotation();
        } else {// don't shoot when out of range
			shoot.shootingActive = false;
		}
    }

    protected override void HandleMovement()
    {

    }

    protected override void HandleRotation()
    {
        Quaternion newRotation = Quaternion.FromToRotation(Vector3.up, direction);
        if(Math.Abs(newRotation.eulerAngles.z) < motionDegree)
		{
			cannon.rotation = newRotation;
			shoot.shootingActive = true;
		} else {// don't shoot when at too high angle
			shoot.shootingActive = false;
		}
	}

    protected void HandleDeath()
    {
        manager.IncreaseScore(100);
		gameObject.SetActive(false);
		shoot.shootingActive = false;
		enabled = false;
	}
}
