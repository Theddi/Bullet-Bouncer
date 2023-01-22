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
    public TMP_Text crystalCountText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
	public UnityEngine.UI.Button resumeButton, quitButton;

	readonly float scoreTickValue = 2f;
	float scoreTicker;

	//Stats
	public GameObject statsPanel;

	//Health Points
	public GameObject healthUI;
	public GameObject healthPointImage;
	float health = 0f;
	float healthMax = 0f;
	float healthbarMaxX;
	float healthbarMaxY;
	float boundingDistance = 5;

	public AudioSource playerAudio;
	public AudioClip crystalCollectClip;
	public int crystalCount = 0;
	public int crystalWinAmount = 10;

	void Start()
	{
		Time.timeScale = 1;
		resumeButton.onClick.AddListener(TriggerPause);
		quitButton.onClick.AddListener(Quit);

		health = playerInstance.GetHealth(); // Number of elements as of player health points
		healthMax = playerInstance.GetHealth();

		healthbarMaxX = ((statsPanel.GetComponent<RectTransform>().sizeDelta.x - 2 * boundingDistance));
		healthbarMaxY = ((statsPanel.GetComponent<RectTransform>().sizeDelta.y - 2 * boundingDistance));

		//Healthbar maximum visualization
		GameObject healthBarMaxImage = Instantiate(healthPointImage, statsPanel.transform);
		healthBarMaxImage.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
		RectTransform healthBarMaxImageTransform = healthBarMaxImage.GetComponent<RectTransform>();
		healthBarMaxImageTransform.sizeDelta = new Vector2(healthbarMaxX, healthbarMaxY);
		healthBarMaxImage.name = "HealthBarMax";

		//Current Healthbar visualization
		GameObject healthBarImage = Instantiate(healthPointImage, statsPanel.transform);
		RectTransform healthBarImageTransform = healthBarImage.GetComponent<RectTransform>();
		healthBarImageTransform.sizeDelta = new Vector2(healthbarMaxX, healthbarMaxY);
		healthBarImage.name = "HealthBar";

		UpdateHealth(health);
	}

	public void Update()
	{
		scoreTicker -= Time.deltaTime;
		if (scoreTicker < 0)
		{
			ScoreTick();
			scoreTicker = scoreTickValue;
		}
		crystalCountText.text = crystalCount+"/"+crystalWinAmount;
		UpdateHealth(playerInstance.GetHealth());

	}
	public void IncreaseScore(int scoreAddition)
    { 
        score += scoreAddition;
        scoreText.text = score.ToString();
    }

	public void UpdateHealth(float health)
	{
		float positiveHealth = health > 0 ? health : 0;
		RectTransform healthBar = GameObject.Find("HealthBar").GetComponent<RectTransform>();
		healthBar.sizeDelta = new Vector2(healthbarMaxX * (health/healthMax), healthbarMaxY);
	}

	void ScoreTick()
	{
		score -= 1;
		scoreText.text = score.ToString();
	}

	public void GameOver()
	{
		Time.timeScale = 0;
		gameOverPanel.SetActive(true);
	}

	public void TriggerPause()
	{
		if (!pausePanel.activeSelf) {
			Time.timeScale = 0;
			pausePanel.SetActive(true);
		} else
		{
			Time.timeScale = 1;
			pausePanel.SetActive(false);
		}
		
	}

	public void IncreaseCrystalCount(int amount){
		if(amount < 0) Debug.LogWarning("Decreasing Crystal count by " + amount);
		crystalCount += amount;
		playerAudio.PlayOneShot(crystalCollectClip);
		if (crystalCount >= crystalWinAmount) Win();
	}
	public void Quit()
	{
		Initiate.Fade("MainScreen", Color.black, .5f);
	}

	public void Win(){
		Time.timeScale = 0;
		Initiate.Fade("CreditScene", Color.black, .5f);
	}
}
