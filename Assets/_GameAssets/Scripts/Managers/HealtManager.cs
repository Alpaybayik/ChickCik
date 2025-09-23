using System;
using UnityEngine;

public class HealtManager : MonoBehaviour
{
    public static HealtManager Instance { get; private set; }
    public event Action OnPlayerDead;

    [Header("References")]
    [SerializeField] private PlayerHealthUI _playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int _maxHealt = 3;
    
    private int _currentHealt;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentHealt = _maxHealt;
    }


    public void Damage(int amount)
    {
        if (_currentHealt > 0)
        {
            _currentHealt -= amount;
            _playerHealthUI.AnimateDamage(amount);

            if (_currentHealt <= 0)
            {
                OnPlayerDead?.Invoke();
                //GameManager.Instance.PlayGameOver();
            }
        }
    }

    public void Heal(int amount)
    {
        if (_currentHealt < _maxHealt)
        {
            _currentHealt = Mathf.Min(_currentHealt + amount, _maxHealt);
        }
    }
}
