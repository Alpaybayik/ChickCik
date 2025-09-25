using System;
using MaskTransitions;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        TransitionManager.Instance.LoadLevel(Consts.Scenes.GAME_SCENE);
        //SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }
    
    private void OnQuitButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        Application.Quit();
    }
}
