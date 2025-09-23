using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlayerState = PlayerState.Idle;

    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }

    public void ChangeState(PlayerState newState)
    {
        if (_currentPlayerState == newState) return;
        _currentPlayerState = newState;
    }

    public PlayerState GetCurrentState() => _currentPlayerState;
}
