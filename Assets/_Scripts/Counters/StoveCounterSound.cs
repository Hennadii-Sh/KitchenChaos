using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter _stoveCounter;

    private AudioSource _audioSource;
    private float _burnShowProgressAmount = .5f;
    private float _warningSoundTimer;
    private float _warningSoundFrequency = 5;
    private bool _playWarningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
        _stoveCounter.OnProgressChanged += _stoveCounter_OnProgressChanged;
    }

    private void _stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        _playWarningSound = _stoveCounter.IsFried() && e._progressNormalized >= _burnShowProgressAmount;
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
            _audioSource.Play();
        else
            _audioSource.Pause();

    }

    private void Update()
    {
        if (_playWarningSound)
        {
            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer <= 0)
            {
                _warningSoundTimer = 1.0f / _warningSoundFrequency;

                SoundManager.Instance.PlayWarningSound(_stoveCounter.transform.position);
            }
        }
    }
}
