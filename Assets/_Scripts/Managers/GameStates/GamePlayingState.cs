using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayingState : IGameState
{
    private float _gamePlayingTimer;

    public GamePlayingState(float gamePlayingTimerMax)
    {
        _gamePlayingTimer = gamePlayingTimerMax;
    }

    public void GameStateHandling()
    {
        _gamePlayingTimer -= Time.deltaTime;
        GameManager.Instance.SetGamePlayingTimer(_gamePlayingTimer);

        if (_gamePlayingTimer <= 0f)
        {
            GameManager.Instance.ChangeGameState(new GameOverState());
        }
    }
}
