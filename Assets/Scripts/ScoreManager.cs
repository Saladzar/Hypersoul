using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int distanceScore = 0;
    private int bounceScore = 0;

    void Start()
    {
        Instance = this;
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
        scoreText.text = "Score: " + (distanceScore + bounceScore);
    }

    public int GetTotalScore()
    {
        return distanceScore + bounceScore;
    }
}
