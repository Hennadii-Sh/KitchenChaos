using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Burning Recipe")]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO _input;
    public KitchenObjectSO _output;
    public float _timerMax;
}
