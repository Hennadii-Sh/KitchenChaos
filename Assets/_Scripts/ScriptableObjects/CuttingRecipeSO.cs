using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Cutting Recipe")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO _input;
    public KitchenObjectSO _output;
    public int _cuttingProgressMax;
}
