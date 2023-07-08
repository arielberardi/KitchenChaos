using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private Transform _kitchenObjectTransform;
    
    public override void Interact(Player player)
    {
    }
}
