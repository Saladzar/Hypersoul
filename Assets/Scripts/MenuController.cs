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
    private bool isInGame = false;

    void Start()
    {
        Instance = this;
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
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    public void OnPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            isPaused = true;
        }
    }

    public void OnQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnDestroy()
    {
        playerInputs.InputMap.Pause.performed -= ctx => OnPause();
        playerInputs.Disable();
    }
}
