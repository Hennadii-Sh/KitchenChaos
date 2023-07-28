using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }
    private void OnDestroy()
    {
        Player.Instance.OnSelectedCounterChange -= Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter == _baseCounter)
        {
            foreach (var _visualGameObject in _visualGameObjectArray)
            {
                _visualGameObject.SetActive(true); 
            }
        } else
        {
            foreach (var _visualGameObject in _visualGameObjectArray)
            {
                _visualGameObject.SetActive(false);
            }
        }
    }

}
