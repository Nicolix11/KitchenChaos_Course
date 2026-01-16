using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        playerInputActions  = new PlayerInputActions(); // create a playerInputObject
        playerInputActions.Player.Enable(); // activate the action map
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // input vector einbinden
        
        inputVector = inputVector.normalized;
        
        return inputVector;
    }
}
