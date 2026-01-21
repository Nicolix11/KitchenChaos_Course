using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject; 
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Spawn Object when Player is not carrying 
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            
            OnPlayerGrabbedObject.Invoke(this, EventArgs.Empty);
        }
    }
}