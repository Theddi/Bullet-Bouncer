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
    float targetRange = 1000f;
    float motionDegree = 250f;

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
	}

    // Update is called once per frame
    void Update()
    {
		direction = target.transform.position - rotationalAxis.position;
        
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
		Quaternion newRotation = Quaternion.FromToRotation(cannonFaceDirection, direction);
		float faceDegree = GetFaceDegree(cannonFaceDirection);
        Quaternion checkRotation = newRotation;
        checkRotation.eulerAngles = checkRotation.eulerAngles + new Vector3(0, 0, motionDegree/2 - faceDegree);
		
		if (Math.Abs(checkRotation.eulerAngles.z) < motionDegree)
		{// Rotate if within motion degree, and enable shooting
			rotationalAxis.rotation = newRotation;
			
			//Debug.Log("Check: "+checkRotation.eulerAngles);
			Debug.Log("Cannon: " + cannon.rotation);
			if (checkRotation.eulerAngles.z < motionDegree/2)
            {// Flip image to right sight
                if(cannon != null)
				{
					//cannon.eulerAngles = new Vector3(0, cannon.eulerAngles.y, cannon.eulerAngles.z);
				} else { Debug.Log("null"); }
					
			} else
			{// Flip image to left sight
				if (cannon != null)
				{
					//cannon.eulerAngles = new Vector3(180, cannon.eulerAngles.y, cannon.eulerAngles.z);
				} else { Debug.Log("null"); }
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
