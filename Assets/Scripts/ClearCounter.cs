using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform _kitchenObject;
    [SerializeField] private Transform _objectSpawnPoint;
    
    public void Interact() 
    {
        Transform kitchenObject = Instantiate(_kitchenObject, _objectSpawnPoint);
        kitchenObject.localPosition = Vector3.zero;
        
        Debug.Log(kitchenObject.GetComponent<KitchenObject>().GetName());
    }
}
