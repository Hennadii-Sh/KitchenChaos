using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveProgressBarAnimationUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private float _burnShowProgressAmount = 0.5f;

    private Animator _animator;
    private const string IS_FLASHING = "IsFlashing";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        _animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        bool show = _stoveCounter.IsFried() && e._progressNormalized >= _burnShowProgressAmount;

        _animator.SetBool(IS_FLASHING, show);
    }

}
