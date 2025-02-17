using System.Collections.Generic;
using UnityEngine;

public class InventorySpawn : MonoBehaviour
{
    [SerializeField] private Transform spawn;

    [SerializeField] private InventoryItemDisplay displayElement;
    public void SpawnList(List<int> ids)
    {
        spawn.DestroyChildren();

        List<InventoryItemDisplay> displayItems = new List<InventoryItemDisplay>();
        foreach (int id in ids)
        {
            var item = Instantiate(displayElement, spawn);
            item.SetupDisplay(id, MaterialInfo.Instance.GetMaterialItem(id).sprite, InventoryInfo.Instance.GetInventoryItem(id).count);
            displayItems.Add(item);
        }

        InventorySearcher.Instance.CreateNewList(displayItems);
    }
}
