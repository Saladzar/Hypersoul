using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        AudioPlayer.Instance.PlayAudioClip(AudioClipType.Enter);
        SceneManager.LoadScene("Game");
    }

    public void OnQuitButtonClicked()
    {
        AudioPlayer.Instance.PlayAudioClip(AudioClipType.Quit);
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
