using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private LayerMask _countersLayermask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private Vector3 _lastInteractionDirection;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            //Destroy(this);
            print("There is more than one Player instance");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;

        //_selectedCounter?.InteractAlternate(this);
        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;

        //_selectedCounter?.Interact(this);
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            _lastInteractionDirection = moveDirection;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractionDirection, out RaycastHit raycastHit, interactDistance, _countersLayermask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        if (_selectedCounter == selectedCounter)
            return;

        _selectedCounter = selectedCounter;

        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs { selectedCounter = _selectedCounter });
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
        if (kitchenObject != null)
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }
    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    public void ClearKitchenObject() => _kitchenObject = null;
    public bool HasKitchenObject() => _kitchenObject != null;
}








/* OLD VERSION (from CodeMonkey) OF Player MOVEMENT HANDLING:

    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _rotationSpeed = 10f;

    private bool _isWalking;

    private void Update()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        MoveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        HandleMovement();
        
        HandleInteractions();

    }

    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = Time.deltaTime * _moveSpeed;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        RotateInMoveDirection(moveDirection);

        RestrictMoveDirection(ref moveDirection);

        MovePlayer(moveDirection);

        _isWalking = moveDirection != Vector3.zero;

        void RestrictMoveDirection(ref Vector3 moveDirection)
        {
            Vector3 directionToTry = moveDirection;

            if (!HaveObstacleOnTheWay(directionToTry))
            {
                moveDirection = directionToTry;
                return;
            }

            if (Mathf.Abs(moveDirection.x) > 0.4f) // <- Special check for input from controller stick
            {
                directionToTry = Vector3.right * moveDirection.x;

                if (!HaveObstacleOnTheWay(directionToTry))
                {
                    moveDirection = directionToTry;
                    return;
                }
            }
            if (Mathf.Abs(moveDirection.z) > 0.4f) // <- Special check for input from controller stick
            {
                directionToTry = Vector3.forward * moveDirection.z;

                if (!HaveObstacleOnTheWay(directionToTry))
                {
                    moveDirection = directionToTry;
                    return;
                }

            }

            moveDirection = Vector3.zero;
            return;
        }

        bool HaveObstacleOnTheWay(Vector3 direction) => Physics.CapsuleCast
            (transform.position, transform.position + Vector3.up * playerHeight, playerRadius, direction, moveDistance);

        void MovePlayer(Vector3 direction) => transform.position += direction * moveDistance;

        // !!!!!!!!!!!!!!!!!!!!!!!!!!
        void RotateInMoveDirection(Vector3 moveDirection)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _rotationSpeed);
            //transform.forward = Vector3.RotateTowards(transform.forward, moveDirection, _rotationSpeed, 1);
        }
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
*/



