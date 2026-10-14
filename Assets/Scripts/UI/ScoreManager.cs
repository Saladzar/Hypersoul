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
    private bool isNewHighScore = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScore();
    }

    void Update()
    {
        distanceScore = Mathf.FloorToInt(Mathf.FloorToInt(player.position.x) / 10 * DifficultyTracker.Instance.GetCurrentDifficulty());
        UpdateScore();
    }

    public void AddBounceScore(int amount)
    {
        bounceScore += Mathf.FloorToInt(amount * DifficultyTracker.Instance.GetCurrentDifficulty());
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (distanceScore + bounceScore > highScore)
            isNewHighScore = true;

        scoreText.text = "Score: " + (distanceScore + bounceScore).ToString().PadLeft(6, '0');
        highScoreText.text = "High Score: " + highScore.ToString().PadLeft(6, '0');
    }

    public int GetTotalScore()
    {
        return distanceScore + bounceScore;
    }

    public bool IsNewHighScore()
    {
        return isNewHighScore;
    }

    public void SaveHighScore()
    {
        if (isNewHighScore)
        {
            PlayerPrefs.SetInt("HighScore", distanceScore + bounceScore);
            PlayerPrefs.Save();
        }
    }
}
