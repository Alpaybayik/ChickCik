using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnMainMenuButtonClicked()
    {
        Debug.Log("Main menu button clicked");
    }

    private void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }
}
