using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private float _footstepTimer;
    private float _footstepTimerMax = 0.1f;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _footstepTimer -= Time.deltaTime;
        if (_footstepTimer < 0)
        {
            _footstepTimer = _footstepTimerMax;

            if (_playerMovement.IsMoving())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepSound(transform.position, volume); 
            }
        }
    }
}
