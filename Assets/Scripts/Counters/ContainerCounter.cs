using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    
    public event EventHandler OnGrabbedObject;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            return;
        }
        
        KitchenObject.SpawnKitchenObject(_kitchenObjectSO, player);
        
        if (OnGrabbedObject != null)
        {
            OnGrabbedObject(this, EventArgs.Empty);
        }
    }
}
