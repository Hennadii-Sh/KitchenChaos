using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        // GamePaused,
        GameOver
    }

    private State _state;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer = 20f;
    [SerializeField] private float _gamePlayingTimerMax = 20f;
    private bool _isGamePaused = false;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;

        _state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (_state == State.WaitingToStart)
        {
            _state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
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
        switch (_state)
        {
            case State.WaitingToStart:
                break;

            case State.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer <= 0f)
                {
                    _gamePlayingTimer = _gamePlayingTimerMax;
                    _state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer <= 0f)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;

            default:
                break;
        }
    }

    public bool IsGamePlaying() => _state == State.GamePlaying;
    public bool IsCountdownToStartActive() => _state == State.CountdownToStart;
    internal bool IsGameOver() => _state == State.GameOver;

    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - _gamePlayingTimer / _gamePlayingTimerMax;
    }
}
