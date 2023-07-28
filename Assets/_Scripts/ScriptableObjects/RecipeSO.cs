using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Recipe")]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> _kitchenObjectSOList;
    public string _recipeName;
}
