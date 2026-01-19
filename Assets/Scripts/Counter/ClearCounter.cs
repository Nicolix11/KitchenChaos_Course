using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    
    public void Interact()
    {
        Debug.Log("Interact");
        
        GameObject kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint.transform);
        kitchenObjectTransform.transform.localPosition = Vector3.zero;

        kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO();
    }
}
