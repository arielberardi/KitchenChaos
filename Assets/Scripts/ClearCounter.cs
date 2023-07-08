using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform _objectTransform;
    [SerializeField] private Transform _objectSpawnPoint;
    [SerializeField] private ClearCounter _secondClearCounter;
    
    private KitchenObject _kitchenObject;

    
    public void Interact(Player player)
    {
        if (_kitchenObject == null)
        {
            Transform objectTransform = Instantiate(_objectTransform, _objectSpawnPoint);
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
