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
		backgroundImage.sizeDelta *= panelRect.sizeDelta.x / backgroundImage.sizeDelta.x;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void HandleDeath()
    {
        gameObject.SetActive(false);
        enabled = false;
    }
}
