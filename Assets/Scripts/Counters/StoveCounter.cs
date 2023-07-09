using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    [SerializeField] private FryingRecipeSO[] _fryingRecipesSO;
    [SerializeField] private BurningRecipeSO[] _buringRecipesSO;
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    
    private FryingRecipeSO _currentFryingRecipe = null;
    private BurningRecipeSO _currentBurningRecipe = null;
    private float fryingTimer = 0f;
    private float burningTimer = 0f;
    private State _state = State.Idle;
    
    private void Update()
    {
        if(!HasKitchenObject())
        {
            return;
        }
        
        switch (_state)
        {
            case State.Frying:
                fryingTimer += Time.deltaTime;
            
                if (OnProgressChanged != null)
                {
                    OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingTimer / _currentFryingRecipe.flytingTimerMax
                    });
                }
                
                if (fryingTimer >= _currentFryingRecipe.flytingTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_currentFryingRecipe.output, this);
                                        
                    burningTimer = 0f;
                    _state = State.Fried;
                    InvokeOnStateChangedEvent();
                    _currentBurningRecipe = GetBurningRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());                    
                }
                
                     
                
                break;
            case State.Fried:
                burningTimer += Time.deltaTime;
                
                if (OnProgressChanged != null)
                {
                    OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burningTimer / _currentBurningRecipe.burningTimerMax
                    });
                }
                
                if (burningTimer >= _currentBurningRecipe.burningTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_currentBurningRecipe.output, this);
                    
                    _state = State.Burned;
                    InvokeOnStateChangedEvent();
                    
                    if (OnProgressChanged != null)
                    {
                        OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0
                        });
                    }
                }
                
                break;
            case State.Idle:
            default:
                break;
        }
    }
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) 
                {
                    player.GetKitchenObject().SetParent(this);
                    
                    fryingTimer = 0f;
                    _state = State.Frying;
                    _currentFryingRecipe = GetFryingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
                    InvokeOnStateChangedEvent();
                    
                    if (OnProgressChanged != null)
                    {
                        OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = fryingTimer / _currentFryingRecipe.flytingTimerMax
                        });
                    }
                }
            }
            else 
            {
                
            }
        }
        else 
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate);
                if (plate && plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestroySelf(); 
                    _state = State.Idle;
                    InvokeOnStateChangedEvent();
                
                    if (OnProgressChanged != null)
                    {
                        OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0
                        });
                    }
                }
            }
            else
            {
                GetKitchenObject().SetParent(player);
                _state = State.Idle;
                InvokeOnStateChangedEvent();
                
                if (OnProgressChanged != null)
                {
                    OnProgressChanged.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = 0
                    });
                }
            }
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO input) 
    {
        return GetFryingRecipeSOForInput(input) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) 
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOForInput(input);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        
        return null;
    }
    
    private FryingRecipeSO GetFryingRecipeSOForInput(KitchenObjectSO input) 
    {
        foreach (FryingRecipeSO fryingRecipeSO in _fryingRecipesSO) 
        {
            if (fryingRecipeSO.input == input) 
            {
                return fryingRecipeSO;
            }
        }
        
        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSOForInput(KitchenObjectSO input) 
    {
        foreach (BurningRecipeSO burnignRecipeSO in _buringRecipesSO) 
        {
            if (burnignRecipeSO.input == input) 
            {
                return burnignRecipeSO;
            }
        }
        
        return null;
    }
    
    private void InvokeOnStateChangedEvent()
    {
        if (OnStateChanged != null)
        {
            OnStateChanged.Invoke(this, new OnStateChangedEventArgs {
                state = _state
            });
        }
    }
}
