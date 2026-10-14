using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;

    private PlayerInputs playerInputs;

    private bool isPaused = false;

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
        playerInputs = new PlayerInputs();
        playerInputs.InputMap.Pause.performed += ctx => OnPause();
        playerInputs.Enable();

        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void OnPlayerLose()
    {
        PlayerAnimations.Instance.Death();
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        GameOverScreen.Instance.ShowGameOverScreen(ScoreManager.Instance.GetTotalScore(), 
                PlayerPrefs.GetInt("HighScore", 0), 
                ScoreManager.Instance.IsNewHighScore());
    }

    public void OnPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            isPaused = false;
        } else
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            isPaused = true;
        }
    }

    public void OnReplay()
    {
        ScoreManager.Instance.SaveHighScore();
        SceneManager.LoadScene("Game");
    }

    public void OnQuit()
    {
        ScoreManager.Instance.SaveHighScore();
        SceneManager.LoadScene("MainMenu");
    }

    void OnDestroy()
    {
        playerInputs.InputMap.Pause.performed -= ctx => OnPause();
        playerInputs.Disable();
    }
}
