using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSoList;
    
    private List<KitchenObjectSO> kitchenObjectSoList;


    private void Awake()
    {
        kitchenObjectSoList = new List<KitchenObjectSO>();
        
    }

    public bool TryAddIngridient(KitchenObjectSO kitchenObjectSo)
    {
        if (!validKitchenObjectSoList.Contains(kitchenObjectSo))
        {
            // not a valid KitchenObject
            return false;
        }
        if (kitchenObjectSoList.Contains(kitchenObjectSo))
        {
            // already has this type
            return false;
        }
        else
        {
            kitchenObjectSoList.Add(kitchenObjectSo);
            return true;
        }
    }
}
