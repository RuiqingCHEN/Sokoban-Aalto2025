using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PageManager : MonoBehaviour
{
    [Header("Pages")]
    public GameObject[] pages;
    
    [Header("Buttons")]
    public Button nextButton;
    public Button previousButton;
    
    [Header("Settings")]
    public int currentPageIndex = 0; 

    void Start()
    {
        ShowPage(currentPageIndex);
    }
    
    public void NextPage()
    {
        currentPageIndex++;
        
        if (currentPageIndex >= pages.Length)
        {
            currentPageIndex = 0;
        }
        
        ShowPage(currentPageIndex);
    }
    
    public void PreviousPage()
    {
        currentPageIndex--;
        if (currentPageIndex < 0)
        {
            currentPageIndex = pages.Length - 1;
        }
        
        ShowPage(currentPageIndex);
    }
    
    void ShowPage(int pageIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        if (pageIndex >= 0 && pageIndex < pages.Length)
        {
            pages[pageIndex].SetActive(true);
        }
    }
}
