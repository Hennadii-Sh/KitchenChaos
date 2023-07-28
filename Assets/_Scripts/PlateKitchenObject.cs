using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAdded_EventArgs> OnIngredientAdded;
    public class OnIngredientAdded_EventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSO_List = new List<KitchenObjectSO>();

    private List<KitchenObjectSO> _kitchenObjectSO_List = new List<KitchenObjectSO>();
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    { return _kitchenObjectSO_List; }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (_validKitchenObjectSO_List.Contains(kitchenObjectSO))
        {
            // Ingredients cannot be repeated in recipes.
            if (_kitchenObjectSO_List.Contains(kitchenObjectSO))
            {
                return false;
            }
            else
            {
                _kitchenObjectSO_List.Add(kitchenObjectSO);
                OnIngredientAdded?.Invoke(this, new OnIngredientAdded_EventArgs { kitchenObjectSO = kitchenObjectSO });
                return true;
            }
        }
        return false;
    }
}
