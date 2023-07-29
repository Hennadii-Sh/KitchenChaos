using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    [SerializeField] private float _gamePlayingTimerMax = 20f;
    public float GamePlayingTimerMax { get => _gamePlayingTimerMax; private set => _gamePlayingTimerMax = value; }

    [SerializeField] private float _countdownToStartTimer = 3f;
    public float CountdownToStartTimer { get => _countdownToStartTimer; private set => _countdownToStartTimer = value; }
    public void SetCountdownToStartTimer(float value) => CountdownToStartTimer = value;

    public IGameState CurrentGameState { get; private set; }
    public bool IsGameWaitingToStart() => CurrentGameState is WaitingToStartState;
    public bool IsCountdownToStartActive() => CurrentGameState is CountdownToStartState;
    public bool IsGamePlaying() => CurrentGameState is GamePlayingState;
    public bool IsGameOver() => CurrentGameState is GameOverState;

    private float _gamePlayingTimer;
    public void SetGamePlayingTimer(float value) => _gamePlayingTimer = value;

    private float _gamePlayingTimerNormalized;
    public float GamePlayingTimerNormalized { get { return 1 - _gamePlayingTimer / GamePlayingTimerMax; } }
    
    private bool _isGamePaused = false;

    protected override void Awake()
    {
        base.Awake();

        ChangeGameState(new WaitingToStartState());
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (IsGameWaitingToStart())
        {
            ChangeGameState(new CountdownToStartState(CountdownToStartTimer));
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        _isGamePaused = !_isGamePaused;

        Time.timeScale = _isGamePaused ? 0f : 1f;

        if (_isGamePaused)
        {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        CurrentGameState.GameStateHandling();
    }

    public void ChangeGameState(IGameState newState)
    {
        CurrentGameState = newState;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
