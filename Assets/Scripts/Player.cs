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
        transform.position +=  moveDir * (Time.deltaTime * moveSpeed);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

        isWalking = moveDir != Vector3.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
