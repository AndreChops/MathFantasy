using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    
    int score = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highscoreText.text = "High Score : " + PlayerPrefs.GetInt("highscore", 0).ToString();
        scoreText.text =  "Score : " + score.ToString();
    }

    public void AddScore()
    {
        score += 10;
        scoreText.text = "Score : " + score.ToString();
        if(score > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
            highscoreText.text = "High Score : " + score.ToString();
        }
    }

    public void AddKillScore()
    {
        score += 50;
        scoreText.text = "Score : " + score.ToString();
        if(score > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
            highscoreText.text = "High Score : " + score.ToString();
        }
    }



    // public void GameOver()
    // {
    //     GameOverScreen.SetActive(true);
    //     highscoreText.text = "Final Score : " + highscore.ToString();
    // }
}
