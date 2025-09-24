using System;
using MaskTransitions;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        _creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        TransitionManager.Instance.LoadLevel(Consts.Scenes.GAME_SCENE);
        //SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }
    
    private void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }

    private void OnCreditsButtonClicked()
    {
        Debug.Log("Credits button clicked");
    }

    private void OnHowToPlayButtonClicked()
    {
        Debug.Log("How to play button clicked");
    }
}
