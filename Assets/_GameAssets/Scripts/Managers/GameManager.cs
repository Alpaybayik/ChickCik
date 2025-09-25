using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    [SerializeField] private CatController _catController;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _loseDelay = 1f;


    private int _currentEggCount = 0;
    private GameState _currentGameState;

    private bool  _isCatCatched = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HealtManager.Instance.OnPlayerDead += HealtManager_OnPlayerDead;
        _catController.OnCatCaught += CatController_OnCatCaught;
    }

    private void CatController_OnCatCaught()
    {
        if (!_isCatCatched)
        {
            _playerHealthUI.AnimateDamage(3);
            StartCoroutine(OnGameOver(true));
            CameraShake.Instance.ShakeCamera(1.5f, 2f, 0.5f);
            _isCatCatched = true;      
        }
    }

    private void HealtManager_OnPlayerDead()
    {
        StartCoroutine(OnGameOver());
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.CutScene);
        BackgroundMusic.Instance.PlayBackgroundMusic(true);
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);
        if (_currentEggCount == _maxEggCount)
        {
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }

    private IEnumerator OnGameOver(bool isCatCatched = false)
    {
        yield return new WaitForSeconds(_loseDelay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();

        if (isCatCatched) {
            AudioManager.Instance.Play(SoundType.CatSound);
        }
    }

    public void ChangeGameState(GameState newState)
    {
        OnGameStateChanged?.Invoke(newState);
        _currentGameState = newState;
        Debug.Log($"Game state changed to {newState}");
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

    public void PlayGameOver()
    {
        StartCoroutine(OnGameOver());
    }
}
