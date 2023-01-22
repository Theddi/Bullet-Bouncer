using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Actor
{
	Player target;
    Transform rotationalAxis; // Cannons rotation transform
    Transform cannon; // Cannons transform
	Vector3 cannonFaceDirection = Vector3.right;
	BulletPool shoot;
	Vector2 direction = Vector2.zero;
    [SerializeField] float targetRange = 20f;
    float motionDegree = 250f;
	bool facingLeft = false;

	// Start is called before the first frame update
	void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        this.speed = 0f;
		rotationalAxis = transform.Find("RotationalAxis");
		cannon = rotationalAxis.Find("Cannon");
		shoot = transform.Find("bulletPool").GetComponent<BulletPool>();
        GetComponent<Damageable>().changeDeathFuntion(HandleDeath);
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), cannon.GetComponent<Collider2D>());
		facingLeft = !IsLeft(cannonFaceDirection);
	}

    // Update is called once per frame
    void Update()
    {
		direction = target.transform.position - rotationalAxis.position;
        
        if (direction.magnitude < targetRange && PlayerApproacher.PlayerInSight(transform, targetRange))
        {
            HandleRotation();
        } else {// don't shoot when out of range
			shoot.shootingActive = false;
		}

		
    }

    protected override void HandleMovement(){}

	protected bool IsLeft(Vector3 direction){
		Quaternion newRotation = Quaternion.FromToRotation(cannonFaceDirection, direction);
		float faceDegree = GetFaceDegree(cannonFaceDirection);
        Quaternion checkRotation = newRotation;
        checkRotation.eulerAngles = checkRotation.eulerAngles + new Vector3(0, 0, motionDegree/2 - faceDegree);
		return checkRotation.eulerAngles.z >= motionDegree/2;
	}

    protected override void HandleRotation()
    {
		Quaternion newRotation = Quaternion.FromToRotation(cannonFaceDirection, direction);
		float faceDegree = GetFaceDegree(cannonFaceDirection);
        Quaternion checkRotation = newRotation;
        checkRotation.eulerAngles = checkRotation.eulerAngles + new Vector3(0, 0, motionDegree/2 - faceDegree);
		
		if (Math.Abs(checkRotation.eulerAngles.z) < motionDegree)
		{// Rotate if within motion degree, and enable shooting
			rotationalAxis.rotation = newRotation;
			
			if (IsLeft(direction))
            {// Flip image to right sight
				if(facingLeft && cannon != null) cannon.Rotate(180,0,0);
				facingLeft = false;
               					
			} else 
			{// Flip image to left sight
				if(!facingLeft && cannon != null) cannon.Rotate(180,0,0);
				facingLeft = true;
			}
			shoot.shootingActive = true;
		} else {// don't shoot when at too high angle
			shoot.shootingActive = false;
		}
	}

	float GetFaceDegree(Vector3 direction)
	{
		if(direction == Vector3.up)
				return 0;
		if (direction == Vector3.down)
				return 180;
		if (direction == Vector3.right)
				return 90;
		if (direction == Vector3.left)
				return 270;
		return 0;
	}


	protected void HandleDeath()
    {
        manager.IncreaseScore(100);
		gameObject.SetActive(false);
		shoot.shootingActive = false;
		enabled = false;
	}
}
