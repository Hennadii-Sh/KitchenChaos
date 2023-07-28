using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Frying Recipe")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO _input;
    public KitchenObjectSO _output;
    public float _timerMax;
}
