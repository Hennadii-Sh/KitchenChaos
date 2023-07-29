using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownToStartState : IGameState
{
    private float _countdownToStartTimer;

    public CountdownToStartState(float countdownToStartTimer)
    {
        _countdownToStartTimer = countdownToStartTimer;
    }

    public void GameStateHandling()
    {
        _countdownToStartTimer -= Time.deltaTime;
        GameManager.Instance.SetCountdownToStartTimer(_countdownToStartTimer);

        if (_countdownToStartTimer <= 0f)
        {
            GameManager.Instance.ChangeGameState(new GamePlayingState(GameManager.Instance.GamePlayingTimerMax));
        }
    }
}
