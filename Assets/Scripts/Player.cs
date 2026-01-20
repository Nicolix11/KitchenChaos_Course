using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; } 
    
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private BaseCounter selectedCounter;
    
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private bool isWalking;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
        }
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction; // Add the Event listener
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
 
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        float interactDistance = 2;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
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

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // cannot move towards moveDir
            
            // attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                // cannot move only on the X
                 
                // attempt only Z movement
                Vector3 moveDirZ = new Vector3(moveDir.z, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // can move on the Z
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position +=  moveDir * (Time.deltaTime * moveSpeed);
        }

        isWalking = moveDir != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        this.selectedCounter = baseCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = baseCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
