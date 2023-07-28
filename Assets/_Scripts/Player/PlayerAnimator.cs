using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] PlayerMovement _playerMovement;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        CheckIsWalking();
    }

    private void Update()
    {
        CheckIsWalking();
    }

    private void CheckIsWalking()
    {
        _animator.SetBool(IS_WALKING, _playerMovement.IsMoving());
    }
}
