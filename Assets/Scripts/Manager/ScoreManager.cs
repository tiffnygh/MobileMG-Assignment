using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : Singleton<ScoreManager>
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score;
    private int highScore;

    public readonly string HIGH_SCORE = "MyGame_HighScore";


    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);
        score = 0; //Reset score
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score.ToString();
        highScoreText.text = "High Score : " + highScore;

    }
        
    public void AddScore(int points)
    {
        // Increase the score
        score += points;

        // Check if the new score is a high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGH_SCORE, highScore);
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
