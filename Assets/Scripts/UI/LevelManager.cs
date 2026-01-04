using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;
    private int highestLevel;

    void Start()
    {
        highestLevel = PlayerPrefs.GetInt("highestLevel", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNum = i + 1;
            if(levelNum > highestLevel)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].interactable = true;

            }
        }
    }

    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Level" + levelNum);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}