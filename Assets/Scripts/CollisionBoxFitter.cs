using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBoxFitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().size;
    }
}
