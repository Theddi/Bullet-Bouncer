using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditText : MonoBehaviour
{
    float creditSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
	}

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector2.up * creditSpeed * Time.deltaTime);
	}
}
