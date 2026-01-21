using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSo;
    
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            //There is a kitchen object on the counter
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSo, this);
        }
    }
    
}
