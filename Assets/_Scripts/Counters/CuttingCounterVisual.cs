using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private CuttingCounter _cuttingCounter;

    private const string CUT = "Cut";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cuttingCounter.OnCut += _cuttingCounter_OnCut;
    }

    private void OnDestroy()
    { 
        _cuttingCounter.OnProgressChanged -= _cuttingCounter_OnCut;
    }

    private void _cuttingCounter_OnCut(object sender, EventArgs e)
    {
            _animator.SetTrigger(CUT);
    }
}
