using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private CuttingRecipeSO[] _cuttingRecipesSO;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetParent(this);
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
        if(HasRecipeWithInput(input))
        {
            KitchenObjectSO output = GetOutputForInput(input);
            GetKitchenObject().DestroySelf();
            
            KitchenObject.SpawnKitchenObject(output, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipesSO) {
            if (cuttingRecipeSO.input == input) {
                return true;
            }
        }
        
        return false;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in _cuttingRecipesSO) {
            if (cuttingRecipeSO.input == input) {
                return cuttingRecipeSO.output;
            }
        }
        
        return null;
    }
}

