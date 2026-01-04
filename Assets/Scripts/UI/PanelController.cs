using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public GameObject menuCanvas;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenMenu()
    {
        if (!menuCanvas.activeSelf && PauseController.IsGamePaused)
        {
            return;
        }
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        PauseController.SetPause(menuCanvas.activeSelf);
    }
}
