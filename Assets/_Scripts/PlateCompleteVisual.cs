using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSOGameObjectList;

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += _plateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        _plateKitchenObject.OnIngredientAdded -= _plateKitchenObject_OnIngredientAdded;
    }

    private void _plateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAdded_EventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
                kitchenObjectSOGameObject.gameObject.SetActive(true);
        }
    }

}
