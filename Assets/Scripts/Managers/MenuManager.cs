using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _exitPrompt;
    [SerializeField] private GameObject _optionsScreen;
    [SerializeField] private GameObject _trackSelectScreen;
    [SerializeField] private GameObject _aboutScreen;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideScreens();
        }
    }

    public void ShowExitPrompt()
    {
        _exitPrompt.SetActive(true);
    }

    public void HideExitPrompt()
    {
        _exitPrompt.SetActive(false);
    }

    public void ShowOptionsScreen()
    {
        _optionsScreen.SetActive(true);
    }

    public void ShowTrackScreen()
    {
        _trackSelectScreen.SetActive(true);
    }

    public void ShowAuthors()
    {
        _aboutScreen.SetActive(true);
    }

    public void HideScreens()
    {
        _aboutScreen.SetActive(false);
        _optionsScreen.SetActive(false);
        _trackSelectScreen.SetActive(false);
        _exitPrompt.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
