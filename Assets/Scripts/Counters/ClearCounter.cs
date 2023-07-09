using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetParent(this);
            }
            else 
            {
                
            }
        }
        else 
        {
            if (player.HasKitchenObject())
            {
                PlateKitchenObject plate = null;
                if (player.GetKitchenObject().TryGetPlate(out  plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf(); 
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plate))
                    {
                        if(plate.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetParent(player);
            }
        }
    }
}
