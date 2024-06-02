using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : Singleton<ScoreManager>
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public int highScore { get; set; }

    private int score { get; set; }

    public readonly string HIGH_SCORE = "MyGame_HighScore";
    private readonly string FIRST_RUN_KEY = "MyGame_FirstRun";


    // Start is called before the first frame update
    void Start()
    {
        CheckFirstRun();
        LoadScore();
        score = 0; //Reset score
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score.ToString();
        highScoreText.text = "High Score : " + highScore.ToString();
        Debug.Log("New High Score: " + highScore);
    }

    private void LoadScore()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }
    public void AddScore(int points)
    {
        // Increase the score
        score += points;

        // Check if the new score is a high score
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGH_SCORE, highScore);
            PlayerPrefs.Save();
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }

    private void CheckFirstRun()
    {
        if (!PlayerPrefs.HasKey(FIRST_RUN_KEY))
        {
            // First run after installation
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt(FIRST_RUN_KEY, 1); // Set the first run flag
            PlayerPrefs.Save(); // Ensure the data is saved
        }
    }
}
