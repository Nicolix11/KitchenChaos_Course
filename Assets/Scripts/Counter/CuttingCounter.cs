using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut; 
    
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;

    private int cuttingProgress;
    
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // there is no kitchenObject on the counter 
            if (player.HasKitchenObject())
            {
                // player is carrying KitchenObject
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
                
                CuttingRecipeSO cuttingRecipeSO = getCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });
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
            cuttingProgress++;
            
            OnCut?.Invoke(this, EventArgs.Empty);
            
            CuttingRecipeSO cuttingRecipeSO = getCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSo = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());
                
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSo, this);
            }
            
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = getCuttingRecipeSoWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    
    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSO = getCuttingRecipeSoWithInput(inputKitchenObjectSo);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO getCuttingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSoArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectSo)
            {
                return cuttingRecipeSo;
            }
        }
        return null;
    }
}
