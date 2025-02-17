using UnityEngine;
using UnityEngine.UI;

public class InventorySpawn : MonoBehaviour
{
    [SerializeField] private Transform spawn;

    [SerializeField] private InventoryItemDisplay displayElement;
    void Start()
    {
        foreach(var item in InventoryInfo.Instance.InventoryData)
            Instantiate(displayElement, spawn).SetupDisplay(item.Key, MaterialInfo.Instance.GetMaterialItem(item.Key).sprite, item.Value.count);
        
    }
}
