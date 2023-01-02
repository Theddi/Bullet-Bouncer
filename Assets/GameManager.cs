using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // User Interface
    public int score = 0;
    public Text scoreText;
    
    public void IncreaseScore(int scoreAddition)
    { 
        score += scoreAddition;
        scoreText.text = score.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
