using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData() => OnAnyObjectPlacedHere = null;

    [SerializeField] protected Transform _counterTopPoint;

    protected KitchenObject _kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Player player)
    {
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
        if (kitchenObject != null)
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
    }
    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    public void ClearKitchenObject() => _kitchenObject = null;
    public bool HasKitchenObject() => _kitchenObject != null;
}
