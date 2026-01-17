using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction; // Eventhandler
    
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        playerInputActions  = new PlayerInputActions(); // create a playerInputObject
        playerInputActions.Player.Enable(); // activate the action map

        playerInputActions.Player.Interact.performed += Interact_Performed; // create an event listener
    }

    private void Interact_Performed(InputAction.CallbackContext obj) // event Trigger Function
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // input vector einbinden
        
        inputVector = inputVector.normalized;
        
        return inputVector;
    }
}
