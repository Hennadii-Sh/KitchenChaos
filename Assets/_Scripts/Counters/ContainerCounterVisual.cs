using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private ContainerCounter _containerCounter;

    private const string OPEN_CLOSE = "OpenClose";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start() => _containerCounter.OnPlayerGrabbedObject += _containerCounter_OnPlayerGrabbedObject;
    private void OnDisable() => _containerCounter.OnPlayerGrabbedObject -= _containerCounter_OnPlayerGrabbedObject;

    private void _containerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
