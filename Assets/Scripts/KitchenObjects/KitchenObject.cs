using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    
    private IKitchenObjectParent _parent;
    
    public Sprite GetSprite() 
    {
        return _kitchenObjectSO.sprite;
    }
    
    public string GetName()
    {
        return _kitchenObjectSO.name;
    }
    
    public Transform GetTransform()
    {
        return transform;
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
}
