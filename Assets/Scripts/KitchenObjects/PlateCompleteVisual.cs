using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSOGameObjects;
    
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    
    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += Plate_OnIngredientAdded;
        
        foreach (KitchenObjectSO_GameObject kitchenSOGameObject in _kitchenObjectSOGameObjects)
        {
            kitchenSOGameObject.gameObject.SetActive(false);
        }
    }
    
    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenSOGameObject in _kitchenObjectSOGameObjects)
        {
            if (kitchenSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
