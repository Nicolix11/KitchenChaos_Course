using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
   
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
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // player is carrying KitchenObject
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
                    CuttingRecipeSO cuttingRecipeSO = getCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        
                    }
                }
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
            if (HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                cuttingProgress++;
                
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                
                CuttingRecipeSO cuttingRecipeSO = getCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSO());

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
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
