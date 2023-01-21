using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
	// Reference
	public Player playerInstance;
    // User Interface
    public int score = 0;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;
	readonly float scoreTickValue = 2f;
	float scoreTicker;

	//Stats
	public GameObject statsPanel;

	//Health Points
	public GameObject healthUI;
	public GameObject healthPointImage;
	public float betweenImageDistance = 20f;
	float numberOfImages = 0f;
	float healthFrameDistanceDenominator = 50;

	public int crystalCount = 0;

	public int crystalWinAmount = 2;

	void Start()
	{
		numberOfImages = playerInstance.GetHealth(); // Number of elements as of player health points
		float healthPointImageWidth = healthPointImage.GetComponent<RectTransform>().sizeDelta.x;

		GameObject healthContainer = Instantiate(healthUI, statsPanel.transform);
		healthContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(healthPointImageWidth * numberOfImages + 50f, statsPanel.GetComponent<RectTransform>().sizeDelta.y);
		healthContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(healthContainer.GetComponent<RectTransform>().sizeDelta.x / 2, healthContainer.GetComponent<RectTransform>().sizeDelta.y / 2);

		float boundingDistance = healthUI.GetComponent<RectTransform>().sizeDelta.x / healthFrameDistanceDenominator; // Distance from frame

		float startingDistance = healthPointImageWidth / 2 + boundingDistance;
		float imagePositioningDistance = ((healthContainer.GetComponent<RectTransform>().sizeDelta.x - 2 * boundingDistance) / numberOfImages);
		if (imagePositioningDistance > healthPointImageWidth + betweenImageDistance)
			imagePositioningDistance = healthPointImageWidth + betweenImageDistance;
		
		// Create the images and position them in the panel
		for (int i = 0; i < numberOfImages; i++)
		{
			GameObject image = Instantiate(healthPointImage, healthContainer.transform);
			image.name = "HealthPoint" + i;
			RectTransform imageRectTransform = image.GetComponent<RectTransform>();
			imageRectTransform.anchoredPosition = new Vector2(startingDistance + i * imagePositioningDistance, 0);
		}
	}

	public void Update()
	{
		scoreTicker -= Time.deltaTime;
		if (scoreTicker < 0)
		{
			scoreTick();
			scoreTicker = scoreTickValue;
		}
		if (numberOfImages > playerInstance.GetHealth())
			DecreaseHealth(playerInstance.GetHealth());
	}
	public void IncreaseScore(int scoreAddition)
    { 
        score += scoreAddition;
        scoreText.text = score.ToString();
    }

	public void DecreaseHealth(float health)
	{
		GameObject inactiveHealth = GameObject.Find("HealthPoint" + (int)health);
		inactiveHealth.SetActive(false);
		numberOfImages = health;
	}

	void scoreTick()
	{
		score -= 1;
		scoreText.text = score.ToString();
	}

	public void GameOver()
	{
		gameOverPanel.SetActive(true);
	}

	public void IncreaseCrystalCount(int amount){
		if(amount < 0) Debug.LogWarning("Decreasing Crystal count by " + amount);
		crystalCount += amount;

		if(crystalCount >= crystalWinAmount) Win();
	}

	public void Win(){
		Initiate.Fade("CreditScene", Color.black, .5f);
	}
}
