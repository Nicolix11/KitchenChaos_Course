using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction; // Eventhandler
    public event EventHandler OnInteractAlternateAction;
    
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        playerInputActions  = new PlayerInputActions(); // create a playerInputObject
        playerInputActions.Player.Enable(); // activate the action map

        playerInputActions.Player.Interact.performed += Interact_Performed; // create an event listener
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
    }

    private void InteractAlternate_Performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
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
