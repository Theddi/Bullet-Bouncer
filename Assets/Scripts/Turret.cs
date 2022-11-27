using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Actor
{

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        initiateStats(1f, 1f, 1f, 1f);
        this.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDeath();
        HandleMovement();
        HandleRotation();
    }

    protected override void HandleMovement()
    {

    }

    protected override void HandleRotation()
    {

    }

    protected override void HandleDeath()
    {

    }
}
