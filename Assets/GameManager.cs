using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // User Interface
    public int score = 0;
    public TMP_Text scoreText;
    
    public void IncreaseScore(int scoreAddition)
    { 
        score += scoreAddition;
        scoreText.text = score.ToString();
    }
}
