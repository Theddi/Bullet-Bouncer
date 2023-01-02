using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // User Interface
    public int score = 0;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;

	public void IncreaseScore(int scoreAddition)
    { 
        score += scoreAddition;
        scoreText.text = score.ToString();
    }

	public void GameOver()
	{
		gameOverPanel.SetActive(true);
	}
}
