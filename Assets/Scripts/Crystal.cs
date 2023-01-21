using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            GameObject.Destroy(gameObject);
            GameManager manager = FindObjectOfType<GameManager>();
            manager.IncreaseCrystalCount(1);
        }
    }
}
