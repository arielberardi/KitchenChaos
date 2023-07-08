using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnGrabbedObject;
    
    [SerializeField] private Transform _kitchenObjectTransform;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            return;
        }
        
        Transform objectTransform = Instantiate(_kitchenObjectTransform);
        objectTransform.GetComponent<KitchenObject>().SetParent(player);
        
        if (OnGrabbedObject != null)
        {
            OnGrabbedObject(this, EventArgs.Empty);
        }
    }
}
