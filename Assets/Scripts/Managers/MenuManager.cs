using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _exitPrompt;
    [SerializeField] private GameObject _optionsScreen;
    [SerializeField] private GameObject _trackSelectScreen;
    [SerializeField] private GameObject _aboutScreen;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private GameObject _pressToContinuePrompt;
    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;


    private void Start()
    {
        AudioController.Instance.PlayMusic(_mainMenuMusic);
        AudioController.Instance.GetMusicVolume(_musicSlider);
        AudioController.Instance.GetSFXVolume(_sfxSlider);
    }

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

    public void EasyTrack()
    {
        _loadingScreen.SetActive(true);
        //SceneManager.LoadSceneAsync(2);
        StartCoroutine(ShowKeyToContinue(2));
    }

    public void HardTrack()
    {
        _loadingScreen.SetActive(true);
        //SceneManager.LoadSceneAsync(1);
        StartCoroutine(ShowKeyToContinue(1));
    }

    IEnumerator ShowKeyToContinue(int sceneIntOrder)
    {
        yield return null;

        _loadingSlider.gameObject.SetActive(true);
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(sceneIntOrder);
        asyncSceneLoad.allowSceneActivation = false;

        while(!asyncSceneLoad.isDone)
        {
            _loadingSlider.value = Mathf.Clamp01(asyncSceneLoad.progress);
            Debug.Log($"Progress: {asyncSceneLoad.progress * 100}%");

            if(asyncSceneLoad.progress >= 0.9f)
            {
                _loadingSlider.gameObject.SetActive(false);
                _pressToContinuePrompt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncSceneLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

}
