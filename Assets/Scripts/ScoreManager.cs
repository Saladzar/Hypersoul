using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int distanceScore = 0;
    private int bounceScore = 0;
    private int highScore = 0;

    void Start()
    {
        Instance = this;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScore();
    }

    void Update()
    {
        distanceScore = Mathf.FloorToInt(player.position.x) / 10;
        UpdateScore();
    }

    public void AddBounceScore(int amount)
    {
        bounceScore += amount;
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (distanceScore + bounceScore > highScore)
        {
            highScore = distanceScore + bounceScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        scoreText.text = "Score: " + (distanceScore + bounceScore);
        highScoreText.text = "High Score: " + highScore;
    }

    public int GetTotalScore()
    {
        return distanceScore + bounceScore;
    }
}
