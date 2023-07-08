using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private CuttingRecipeSO[] _cuttingRecipesSO;
    
    private int _cuttingProgress = 0;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetParent(this);
                    _cuttingProgress = 0;
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
                
            }
            else
            {
                GetKitchenObject().SetParent(player);
            }
        }
    }
    
    public override void InteractAlternate(Player player) {
        if (!HasKitchenObject()) 
        {
            return;   
        }

        KitchenObjectSO input = GetKitchenObject().GetKitchenObjectSO();
        
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(input);
        if(!cuttingRecipeSO)
        {
            return;
        }
        
        _cuttingProgress++;
        
        if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
        {
            KitchenObjectSO output = GetOutputForInput(input);
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(output, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        return GetCuttingRecipeSOForInput(input) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(input);

        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        
        return null;
    }
    
    private CuttingRecipeSO GetCuttingRecipeSOForInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipesSO) 
        {
            if (cuttingRecipeSO.input == input) 
            {
                return cuttingRecipeSO;
            }
        }
        
        return null;
    }
}

