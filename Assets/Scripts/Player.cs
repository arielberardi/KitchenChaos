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

        bool canMove = getCanMove(moveDirection);
        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0);

            canMove = getCanMove(moveDirectionX);
            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z);

                canMove = getCanMove(moveDirectionZ);
                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += _speed * Time.deltaTime * moveDirection.normalized;
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

    private bool getCanMove(Vector3 direction)
    {
        return !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * _height,
            _radious,
            direction,
            _speed * Time.deltaTime
        );
    }
}
