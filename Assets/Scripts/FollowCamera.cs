using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 temp = GameObject.Find("Camera").transform.position;
        transform.position = new Vector3(temp.x, temp.y, transform.position.z);
    }
}
