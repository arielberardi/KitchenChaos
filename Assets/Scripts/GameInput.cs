using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{   
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    
    private PlayerInputActions _playerInputActions;
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Interact.performed += Interact_OnPerformed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_OnPerformed;
    }
    
    public Vector2 GetInputVector()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }
    
    private void Interact_OnPerformed(InputAction.CallbackContext obj) 
    {
        if (OnInteractAction != null) 
        {
            OnInteractAction(this, EventArgs.Empty);
        }
    }
    
    private void InteractAlternate_OnPerformed(InputAction.CallbackContext obj) 
    {
        if (OnInteractAlternateAction != null) 
        {
            OnInteractAlternateAction(this, EventArgs.Empty);
        }
    }
}
