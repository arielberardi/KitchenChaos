using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 7.0f;
    [SerializeField] private float _rotationSpeed = 10.0f;
    [SerializeField] private float _height = 2.0f;
    [SerializeField] private float _radious = 0.7f;
    [SerializeField] private float _interactDistance = 2.0f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _counterLayerMask;

    private bool _isWalking = false;
    private Vector3 lastInteractDirection = Vector3.zero;

    private void Start() 
    {
        _gameInput.OnInteractEvent += GameInputOnInteractAction;
    }

    private void Update()
    {
        HandleMovements();
        // HandleInteractions();
    }

    public bool GetIsWalking()
    {
        return _isWalking;
    }

    private bool GetIsCastIntercepted(Vector3 direction)
    {
        return Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * _height,
            _radious,
            direction,
            _speed * Time.deltaTime
        );
    }

    private Vector3 GetMoveDirection()
    {
        Vector2 inputVector = _gameInput.GetInputVector();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        return moveDirection;
    }

    private void HandleMovements()
    {
        Vector3 moveDirection = GetMoveDirection();

        bool canMove = !GetIsCastIntercepted(moveDirection);
        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0);

            canMove = !GetIsCastIntercepted(moveDirectionX);
            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z);

                canMove = !GetIsCastIntercepted(moveDirectionZ);
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

    private void HandleInteractions()
    {
        Vector3 moveDirection = GetMoveDirection();
        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        bool isInteracting = Physics.Raycast(
            transform.position,
            lastInteractDirection,
            out RaycastHit raycastHit,
            _interactDistance,
            _counterLayerMask
        );
        
        if (!isInteracting) 
        {
            return;
        }
        
        if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
            clearCounter.Interact();    
        }   
    }
    
    private void GameInputOnInteractAction(object sender, System.EventArgs e)
    {
        HandleInteractions();
    }
}
