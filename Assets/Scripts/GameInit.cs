using UnityEngine;

public class GameInit : MonoBehaviour
{
    private static bool hasInitialized = false;

    void Awake()
    {
        if (hasInitialized)
        {
            Destroy(gameObject);
            return;
        }
        hasInitialized = true;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}