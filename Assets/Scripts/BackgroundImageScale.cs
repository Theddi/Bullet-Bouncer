using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImageScale : MonoBehaviour
{
    RectTransform backgroundImage;
    RectTransform panelRect;
    // Start is called before the first frame update
    void Start()
    {
		backgroundImage = GetComponent<RectTransform>();
		panelRect = transform.parent.GetComponent<RectTransform>();
        
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
