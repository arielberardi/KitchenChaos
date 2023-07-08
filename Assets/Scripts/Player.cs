using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float _speed = 7.0f;
    [SerializeField] private float _rotationSpeed = 10.0f;
    [SerializeField] private float _height = 2.0f;
    [SerializeField] private float _radious = 0.7f;
    [SerializeField] private float _interactDistance = 2.0f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _counterLayerMask;
    [SerializeField] private Transform _objectSpawnPoint;
    
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }
    
    private bool _isWalking = false;
    private Vector3 lastInteractDirection = Vector3.zero;
    private BaseCounter _counterSelected;
    private KitchenObject _kitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance.");
        }
        
        Instance = this;
    }

    private void Start() 
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
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
            _counterSelected.Interact(this);
        }
    }
    
    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if (_counterSelected != null)
        {
            _counterSelected.InteractAlternate(this);
        }
    }

    private void HandleMovements()
    {
        Vector3 moveDirection = GetMoveDirection();

        bool canMove = !GetIsCastIntercepted(moveDirection);
        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0);
            canMove = moveDirectionX.x != 0 && !GetIsCastIntercepted(moveDirectionX);
            
            // Attemp movement on X axis
            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z);
                canMove = moveDirectionZ.z != 0 && !GetIsCastIntercepted(moveDirectionZ);
                
                // Attemp movement on Z axis
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
    
        _isWalking = moveDirection != Vector3.zero;
        if (_isWalking)
        {
            transform.forward = Vector3.Slerp(
                transform.forward,
                moveDirection,
                Time.deltaTime * _rotationSpeed
            );
        }
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
        
        BaseCounter baseCounter = null;
        if (isInteracting) 
        {
            raycastHit.transform.TryGetComponent(out baseCounter);
        }

        UpdateSelectedCounter(baseCounter );
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
        return new Vector3(inputVector.x, 0, inputVector.y).normalized;
    }

    private void UpdateSelectedCounter(BaseCounter counterSelected)
    {
        if (_counterSelected == counterSelected)
        {
            return;
        }
        
        _counterSelected = counterSelected;
                
        if (OnSelectedCounterChanged != null) 
        {
            OnSelectedCounterChanged(this, new OnSelectedCounterChangedEventArgs {
                selectedCounter = _counterSelected
            });
        }
    }
    
    public Transform GetObjectSpawnPoint()
    {   
        return _objectSpawnPoint; 
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }
      
    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    
    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
    
    public bool HasKitchenObject()
    {
        return _kitchenObject != null;        
    }
}
