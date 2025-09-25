using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        //_gameManager.ChangeGameState(GameState.CutScene);
    }

    private void OnEnable()
    {
        _playableDirector.Play();

        _playableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        GameManager.Instance.ChangeGameState(GameState.Play);
    }
}
