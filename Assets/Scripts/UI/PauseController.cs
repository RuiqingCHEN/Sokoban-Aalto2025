using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; } = false;
    public static void SetPause(bool pause)
    {
        IsGamePaused = pause;
    }

    static PauseController()
    {
        SceneManager.sceneLoaded += (scene, mode) => {
            IsGamePaused = false;
            Time.timeScale = 1f; // 确保时间恢复
        };
    }
}

