using System;
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
    
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }
    
    private bool _isWalking = false;
    private Vector3 lastInteractDirection = Vector3.zero;
    private ClearCounter _counterSelected;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance.");
        }
        
        Instance = this;
    }

    private void Start() 
    {
        _gameInput.OnInteractEvent += GameInput_OnInteractAction;
    }

    private void Update()
    {
        HandleMovements();
        HandleInteractions();
    }

    public bool GetIsWalking()
    {
        return _isWalking;
    }
    
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (_counterSelected != null)
        {
            _counterSelected.Interact();
        }
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
            SetCounterSelected(null);
            return;
        }
        
        if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
            if (clearCounter != _counterSelected) 
            {
                SetCounterSelected(clearCounter);
            }   
        }  
        else 
        {
            SetCounterSelected(null);
        } 
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
        return new Vector3(inputVector.x, 0, inputVector.y);
    }

    private void SetCounterSelected(ClearCounter counterSelected)
    {
        _counterSelected = counterSelected;
                
        if (OnSelectedCounterChanged != null) 
        {
            OnSelectedCounterChanged(this, new OnSelectedCounterChangedEventArgs {
                selectedCounter = _counterSelected
            });
        }
    }
}
