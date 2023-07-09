using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _plateKitchenObjectSO;
    [SerializeField] private float _spawnPlateSpeed = 4.0f;
    [SerializeField] private int _maxSpawnedPlates = 4;
    
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private float _spawnPlatesTimer = 0f;
    private int _spawnedPlatesCount = 0;
    
    private void Update()
    {
        _spawnPlatesTimer += Time.deltaTime;
        
        if (_spawnPlatesTimer >= _spawnPlateSpeed)        
        {
            _spawnPlatesTimer = 0;
                
            if (_spawnedPlatesCount < _maxSpawnedPlates)
            {
                _spawnedPlatesCount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (_spawnedPlatesCount > 0)
            {
                _spawnedPlatesCount--;

                KitchenObject.SpawnKitchenObject(_plateKitchenObjectSO, player);
                
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
