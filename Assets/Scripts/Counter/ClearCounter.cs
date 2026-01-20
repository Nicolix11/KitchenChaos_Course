using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // there is no kitchenObject on the counter 
            if (player.HasKitchenObject())
            {
                // player is carrying KitchenObject
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // player not carrying a KitchenObject 
            }
        }
        else
        {
            // there is a kitchenObject on the counter 
            if (player.HasKitchenObject())
            {
                // player is carrying KitchenObject
            }
            else
            {
                // player is not carrying KitchenObject
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
