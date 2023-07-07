using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 7.0f;
    [SerializeField] private float _rotationSpeed = 10.0f;
    [SerializeField] GameInput _gameInput;
    
    private bool _isWalking = false;
    
    private void Update()
    {
        Vector2 inputVector = _gameInput.GetInputVector();
        
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += _speed * Time.deltaTime * moveDirection;
        
        _isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _rotationSpeed);
    }
    
    public bool isWalking()
    {
        return _isWalking;
    }
}
