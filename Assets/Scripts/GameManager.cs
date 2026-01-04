using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool HasWon { get; private set; } = false;
    public int totalTargets;
    public int finishedBoxs;
    [Header("Win UI")]
    public GameObject winUIPanel;
    public TextMeshProUGUI countdownText;
    [Header("End Game UI")]
    public GameObject endGamePanel;

    private void Start()
    {
        totalTargets = GameObject.FindGameObjectsWithTag("Target").Length;

        if (winUIPanel != null)
        {
            winUIPanel.SetActive(false);
        }
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }
    }
    private void Awake()
    {
        HasWon = false;
    }
    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            HasWon = false;
            ResetLevel();
        }
    }

    public void CheckFinish()
    {
        if (finishedBoxs == totalTargets)
        {
            HasWon = true;
            print("YOU WIN!");

            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int highestLevel = PlayerPrefs.GetInt("highestLevel", 1);

            if (currentIndex >= highestLevel)
            {
                PlayerPrefs.SetInt("highestLevel", currentIndex + 1);
                PlayerPrefs.Save();
            }

            if (endGamePanel != null)
            {
                ShowEndGameUI();
            }
            else
            {
                ShowWinUI();
            }
        }
    }
    void ShowWinUI()
    {
        if (winUIPanel != null)
        {
            winUIPanel.SetActive(true);
            StartCoroutine(CountdownAndLoadNextLevel());
        }
    }
    void ShowEndGameUI()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
        }
    }
    IEnumerator CountdownAndLoadNextLevel()
    {
        int countdown = 2;
        while (countdown > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = countdown + "s";
            }

            yield return new WaitForSeconds(1f);
            countdown--;
        }
        if (countdownText != null)
        {
            countdownText.text = "0s";
        }
        yield return new WaitForSeconds(0.2f);
        if (winUIPanel != null)
        {
            winUIPanel.SetActive(false);
        }
        LoadNextLevel();
    }
    void ResetLevel()
    {
        if (winUIPanel != null)
        {
            winUIPanel.SetActive(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (nextIndex >= sceneCount)
        {
            return;
        }
        HasWon = false;
        SceneManager.LoadScene(nextIndex);
    }

    public void CloseEndGamePanel()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }
    }
}
