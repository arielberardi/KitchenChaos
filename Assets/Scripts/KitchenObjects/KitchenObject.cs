using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    
    private IKitchenObjectParent _parent;
    
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSO;
    }
    
    public void SetParent(IKitchenObjectParent parent)
    {
        if (_parent != null)
        {
            _parent.ClearKitchenObject();
        }
        
        _parent = parent;
        if (_parent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject");
        }
        
        _parent.SetKitchenObject(this);
        
        transform.parent = _parent.GetObjectSpawnPoint();
        transform.localPosition = Vector3.zero;
    }
    
    public IKitchenObjectParent GetParent()
    {
        return _parent;
    }
    
    public void DestroySelf()
    {
        _parent.ClearKitchenObject();
        Destroy(gameObject);
    }
    
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO objectSO, IKitchenObjectParent parent)
    {
        KitchenObject kitchenObject = Instantiate(objectSO.prefab).GetComponent<KitchenObject>();
        kitchenObject.SetParent(parent);
        
        return kitchenObject;
    }
}
