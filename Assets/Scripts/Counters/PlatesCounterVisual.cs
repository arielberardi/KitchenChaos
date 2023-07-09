using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter _platesCounter;
    [SerializeField] private Transform _platesPrefab;
    [SerializeField] private Transform _platesSpawnPoint;

    private List<GameObject> _platesVisualList;

    private void Awake()
    {
        _platesVisualList = new List<GameObject>();
    }

    private void Start()
    {
        _platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        _platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }
    
    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) 
    {
        Transform plateVisual =  Instantiate(_platesPrefab, _platesSpawnPoint);
        
        float plateOffsetY = 0.1f *  _platesVisualList.Count;
        plateVisual.localPosition = new Vector3(0, plateOffsetY, 0);
        
        _platesVisualList.Add(plateVisual.gameObject);    
    }
    
    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateVisual = _platesVisualList[_platesVisualList.Count - 1];
        _platesVisualList.Remove(plateVisual);
        Destroy(plateVisual);
    }
}
