using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;
    
    public void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }   
    
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried)
        {
            stoveOnGameObject.SetActive(true);
            particlesGameObject.SetActive(true);
        }
        else 
        {
            stoveOnGameObject.SetActive(false);
            particlesGameObject.SetActive(false);
        }
    }
}
