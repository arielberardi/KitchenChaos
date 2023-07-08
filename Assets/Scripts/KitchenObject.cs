using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Review this, it has a circula reference between KitchenObjectSO -> Prefab and Prefab -> KitchenObjectSO
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    
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
}
