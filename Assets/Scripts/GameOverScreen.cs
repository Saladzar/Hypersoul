using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button quitButton;

    public void OnQuitButtonClicked()
    {
        MenuController.Instance.OnQuit();
    }
}
