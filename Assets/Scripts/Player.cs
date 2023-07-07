using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;

    [SerializeField]
    private float _rotationSpeed = 10.0f;

    [SerializeField]
    private float _height = 2.0f;

    [SerializeField]
    private float _radious = 0.7f;

    [SerializeField]
    GameInput _gameInput;

    private bool _isWalking = false;

    private void Update()
    {
        Vector2 inputVector = _gameInput.GetInputVector();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = _speed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * _height,
            _radious,
            moveDirection,
            moveDistance
        );

        if (!canMove)
        {
            // Retry on X direction
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * _height,
                _radious,
                moveDirectionX,
                moveDistance
            );
            
            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else 
            {
                // Retry on Z direction
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized  ;
                canMove = !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * _height,
                    _radious,
                    moveDirectionZ,
                    moveDistance
                );
                
                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDistance * moveDirection;
        }

        transform.forward = Vector3.Slerp(
            transform.forward,
            moveDirection,
            Time.deltaTime * _rotationSpeed
        );

        _isWalking = moveDirection != Vector3.zero;
    }

    public bool isWalking()
    {
        return _isWalking;
    }
}
