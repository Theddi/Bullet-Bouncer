using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenText : MonoBehaviour
{
	// Main Text Fading Animation
    TMP_Text textBox;
	byte textTransparency = 255;
	byte redPart = 255;
	byte bluePart = 255;
	byte greenPart = 255;
	byte lowerTransparency = 10;
	bool animateDown = true;
	bool sceneLoad = false;

	private void Awake()
	{
		textBox = GetComponent<TMP_Text>();
	}

	// Start is called before the first frame update
	void Start()
    {
        textBox = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.anyKey)
		{
			redPart = 100;
			bluePart = 100;
			greenPart = 255;
			sceneLoad = true;
		}

		textBox.faceColor = new Color32(redPart, greenPart, bluePart, textTransparency);
		if (textTransparency > lowerTransparency && animateDown && !sceneLoad)
		{
			textTransparency--;
		} else
		{
			animateDown = false;
		}
		if (!animateDown && textTransparency < 255 && !sceneLoad) 
		{
			textTransparency++;
		} else
		{
			animateDown = true;
		}
	}
}
