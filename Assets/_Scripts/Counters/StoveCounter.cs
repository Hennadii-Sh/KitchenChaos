using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] FryingRecipeSO[] _fryingRecipesSO_Array;
    [SerializeField] BurningRecipeSO[] _burningRecipesSO_Array;

    private State _state;
    private float _timerCurrent = 0f;
    private FryingRecipeSO _currentFryingRecipeSO;
    private BurningRecipeSO _currentBurningRecipeSO;

    private void Start()
    {
        _state = State.Idle;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;

                case State.Frying:
                    _timerCurrent += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        _progressNormalized = _timerCurrent / _currentFryingRecipeSO._timerMax
                    });

                    if (_timerCurrent >= _currentFryingRecipeSO._timerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(_currentFryingRecipeSO._output, this);

                        _currentBurningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        _state = State.Fried;

                        // FIX of null reference exception if we do not have correct burning recipe.
                        if (_currentBurningRecipeSO == null)
                        {
                            _state = State.Burned;

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                            {
                                _progressNormalized = 0f
                            });
                        }

                        _timerCurrent = 0f;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                    }
                    break;

                case State.Fried:
                    _timerCurrent += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        // _currentFryingRecipeSO null value will cause null reference exception (FIXED)
                        _progressNormalized = _timerCurrent / _currentBurningRecipeSO._timerMax
                    });

                    // _currentFryingRecipeSO null value will cause null reference exception (FIXED)
                    if (_timerCurrent >= _currentBurningRecipeSO._timerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        // _currentFryingRecipeSO null value will cause null reference exception (FIXED)
                        KitchenObject.SpawnKitchenObject(_currentBurningRecipeSO._output, this);

                        _state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            _progressNormalized = 0f
                        });
                    }
                    break;

                case State.Burned:
                    break;
                default:
                    break;
            }
        }
    }


    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);

            _currentFryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());


            _state = State.Frying;
            _timerCurrent = 0f;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                _progressNormalized = _timerCurrent / _currentFryingRecipeSO._timerMax
            });
        }
        else if (HasKitchenObject())
        {

            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                _state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    _progressNormalized = 0f
                });
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                    _state = State.Idle;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        _progressNormalized = 0f
                    });
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO._output;
        }
        return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in _fryingRecipesSO_Array)
        {
            if (fryingRecipeSO._input == inputKitchenObjectSO)
                return fryingRecipeSO;
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in _burningRecipesSO_Array)
        {
            if (burningRecipeSO._input == inputKitchenObjectSO)
                return burningRecipeSO;
        }
        return null;
    }

    public bool IsFried() => _state == State.Fried;
}
