using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{   
    public event EventHandler OnInteractEvent;
    
    private PlayerInputActions _playerInputActions;
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Interact.performed += InteractPerformed;
    }
    
    public Vector2 GetInputVector()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }
    
    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        if (OnInteractEvent != null) 
        {
            OnInteractEvent(this, EventArgs.Empty);
        }
    }
}
