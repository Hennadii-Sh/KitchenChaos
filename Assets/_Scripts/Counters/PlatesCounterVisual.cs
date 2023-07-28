using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;
    [SerializeField] private PlatesCounter _platesCounter;

    private List<GameObject> _plateVisualGameObjectList = new List<GameObject>();

    private void Start()
    {
        _platesCounter.OnPlateSpawned += _platesCounter_OnPlateSpawned;
        _platesCounter.OnPlateRemoved += _platesCounter_OnPlateRemoved;
    }

    private void OnDestroy()
    {
        _platesCounter.OnPlateSpawned -= _platesCounter_OnPlateSpawned;
        _platesCounter.OnPlateRemoved -= _platesCounter_OnPlateRemoved;
    }

    private void _platesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualGameObjectList.Count, 0);

        _plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void _platesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
        _plateVisualGameObjectList.Remove(plateGameObject);

        Destroy(plateGameObject);
    }

}
