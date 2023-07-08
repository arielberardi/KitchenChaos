using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private Transform _kitchenObjectTransform;
    [SerializeField] private Transform _objectSpawnPoint; // TODO: We need to check we can get internaly without referencing
    
    private KitchenObject _kitchenObject;

    public override void Interact(Player player)
    {
        if (_kitchenObject == null)
        {
            Transform objectTransform = Instantiate(_kitchenObjectTransform, _objectSpawnPoint);
            objectTransform.GetComponent<KitchenObject>().SetParent(this);
        }
        else 
        {
            _kitchenObject.SetParent(player);
        }   
    }
    
    public Transform GetObjectSpawnPoint()
    {
        return _objectSpawnPoint; 
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }
      
    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    
    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
    
    public bool HasKitchenObject()
    {
        return _kitchenObject != null;        
    }
}
