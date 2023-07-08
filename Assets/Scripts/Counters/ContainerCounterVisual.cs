using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter _containerCounter;
    
    private const string OPEN_CLOSE = "OpenClose";
    
    private Animator _animator;
    
    private void Awake() 
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        _containerCounter.OnGrabbedObject += ContainerCounter_OnGrabbedObject;
    }
    
    private void ContainerCounter_OnGrabbedObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
