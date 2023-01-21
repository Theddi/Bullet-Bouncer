using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditText : MonoBehaviour
{
    TMP_Text creditText;
	RectTransform textTransform;
	float creditSpeed = 50;

	public TextAsset textFile;     // drop your file here in inspector
	float timePassed;
	void Start()
	{
		creditText = GetComponent<TMP_Text>();
		textTransform = GetComponent<RectTransform>();

		string text = textFile.text;  //this is the content as string
		creditText.text = text;
	}

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector2.up * creditSpeed * Time.deltaTime);
		timePassed += Time.deltaTime;
		if ((transform.position.y - creditText.textBounds.size.y * 1.5) > transform.parent.position.y && timePassed > 2 || Input.anyKey)
		{ // When Text is out of screen
			Initiate.Fade("MainScreen", Color.black, .5f);
		}
	}
}
