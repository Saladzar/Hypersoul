using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private TextMeshProUGUI highScoreValue;
    [SerializeField] private TextMeshProUGUI highScoreLabel;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowGameOverScreen(int score, int highScore, bool isNewHighScore)
    {
        scoreValue.text = score.ToString().PadLeft(6, '0');

        if (isNewHighScore)
        {
            highScoreValue.text = highScore.ToString().PadLeft(6, '0');
            TextThrobber.Instance.StartThrobbing();
        }
        else
            highScoreValue.text = highScore.ToString().PadLeft(6, '0');

        newHighScoreText.gameObject.SetActive(isNewHighScore);
    }

    public void OnReplayButtonClicked()
    {
        AudioPlayer.Instance.PlayAudioClip(AudioClipType.Enter);
        MenuController.Instance.OnReplay();
    }

    public void OnQuitButtonClicked()
    {
        AudioPlayer.Instance.PlayAudioClip(AudioClipType.Quit);
        MenuController.Instance.OnQuit();
    }
}
