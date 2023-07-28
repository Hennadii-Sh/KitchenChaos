using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Singleton<DeliveryManager>
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    [SerializeField] RecipesListSO _recipeListSO;
    [SerializeField] private float _spawnRecipeTimerMax = 4f;
    [SerializeField] private int _waitingRecipesMax = 4;

    private List<RecipeSO> _waitingRecipesSOList = new();
    private float _spawnRecipeTimer;
    private int _successfulDeliverysAmount = 0;

    public List<RecipeSO> GetWaitingRecipeSOList() => _waitingRecipesSOList;
    public int GetSuccessfulDelyverysAmount() => _successfulDeliverysAmount;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;

        SpawnRecipes();
    }

    private void SpawnRecipes()
    {
        if (_spawnRecipeTimer > 0)
            return;

        _spawnRecipeTimer = _spawnRecipeTimerMax;

        if (!GameManager.Instance.IsGamePlaying() || _waitingRecipesSOList.Count >= _waitingRecipesMax)
            return;

        RecipeSO waitingRecipeSO = _recipeListSO._recipeSOList[UnityEngine.Random.Range(0, _recipeListSO._recipeSOList.Count)];

        _waitingRecipesSOList.Add(waitingRecipeSO);

        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO waitingRecipeSO in _waitingRecipesSOList)
        {
            if (IsPlateContentMatchesRecipe(waitingRecipeSO, plateKitchenObject))
            {
                SuccessfulDeliveryHandling(waitingRecipeSO);
                return;
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        print("Player delivered the incorrect recipe!");
    }

    private void SuccessfulDeliveryHandling(RecipeSO waitingRecipeSO)
    {
        _successfulDeliverysAmount++;
        _waitingRecipesSOList.Remove(waitingRecipeSO);

        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    }

    private bool IsPlateContentMatchesRecipe(RecipeSO recipeSO, PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> recipeKitchenObjectSO_List = recipeSO._kitchenObjectSOList;
        List<KitchenObjectSO> plateKitchenObjectSO_List = plateKitchenObject.GetKitchenObjectSOList();

        if (recipeKitchenObjectSO_List.Count != plateKitchenObjectSO_List.Count)
            return false;

        // Ограничение: Ingredients can't be repeated in recipes. Иначе (Tomato+Tomato+Cabbage == Tomato+Cabbage+Cabbage)->true
        bool isComparisonSuccessful = false;

        foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObjectSO_List)
        {
            isComparisonSuccessful = false;

            foreach (KitchenObjectSO recipeKitchenObjectSO in recipeKitchenObjectSO_List)
            {

                isComparisonSuccessful = plateKitchenObjectSO == recipeKitchenObjectSO;

                if (isComparisonSuccessful)
                    break;
            }

            if (!isComparisonSuccessful)
                break;
        }
        return isComparisonSuccessful;
    }

}
