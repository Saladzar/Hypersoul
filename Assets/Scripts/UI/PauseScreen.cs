using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    public void OnResumeButtonClicked()
    {
        AudioPlayer.Instance.PlayAudioClip(AudioClipType.Enter);
        MenuController.Instance.OnPause();
    }

    public void OnQuitButtonClicked()
    {
        AudioPlayer.Instance.PlayAudioClip(AudioClipType.Quit);
        MenuController.Instance.OnQuit();
    }
}
