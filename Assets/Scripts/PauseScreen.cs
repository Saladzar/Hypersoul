using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    public void OnResumeButtonClicked()
    {
        MenuController.Instance.OnPause();
    }

    public void OnQuitButtonClicked()
    {
        MenuController.Instance.OnQuit();
    }
}
