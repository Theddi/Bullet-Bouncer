using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bullet_speed = 20f;
    int bounces = 0;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollision()
    {
        bounces++;
    }
}
