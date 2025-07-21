using System;
using System.Collections;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockedCharacters;
    public GameObject[] unlockCharacters;
    public GameObject UINotice;

    enum Achive { UnlockPotato, UnlockBean }
    Achive[] achives;
    WaitForSecondsRealtime wait;

    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);

        if (PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0); // Initialize all achievements to 0
        }
    }
    void Start()
    {
        UnlockCharacter(); // Check and unlock characters based on achievements    
    }

    void UnlockCharacter()
    {
        for (int index = 0; index < lockedCharacters.Length; index++)
        {
            string achiveName = achives[index].ToString();
            bool isAchive = PlayerPrefs.GetInt(achiveName) == 1;
            lockedCharacters[index].SetActive(!isAchive);
            unlockCharacters[index].SetActive(isAchive);
        }
    }

    void LateUpdate()
    {
        foreach (Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato:
                isAchive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1); // Mark the achievement as completed

            for (int index = 0; index < UINotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;

                UINotice.transform.GetChild(index).gameObject.SetActive(isActive); // Deactivate all child objects of UINotice
            }

            StartCoroutine(NoticeCoroutine()); // Show the notice UI for the achievement
        }
    }

    IEnumerator NoticeCoroutine()
    {
        UINotice.SetActive(true); // Show the notice UI

        yield return wait; // Wait for 5 seconds
        
        UINotice.SetActive(false); // Hide the notice UI
    }
}
