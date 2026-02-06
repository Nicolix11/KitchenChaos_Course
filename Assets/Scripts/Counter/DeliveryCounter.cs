using System;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenobject))
            {
                // only accepts plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenobject);
                
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
