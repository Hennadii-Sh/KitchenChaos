using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _particleGameObject;

    private void Start()
    {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
    }

    private void OnDestroy()
    {
        _stoveCounter.OnStateChanged -= _stoveCounter_OnStateChanged;
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        _stoveOnGameObject.SetActive(showVisual);
        _particleGameObject.SetActive(showVisual);
    }
}
