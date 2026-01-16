using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    
    private void Update()
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
}
