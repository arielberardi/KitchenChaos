using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectsSO;
    private List<KitchenObjectSO> _kitchenObjectsSO;
    
    public void Awake()
    {
        _kitchenObjectsSO = new List<KitchenObjectSO>();
    }
    
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!_validKitchenObjectsSO.Contains(kitchenObjectSO))
        {
            return false;
        }
        
        if (_kitchenObjectsSO.Contains(kitchenObjectSO))
        {
            return false;
        }
        
        _kitchenObjectsSO.Add(kitchenObjectSO);
        return true;
    }
}
