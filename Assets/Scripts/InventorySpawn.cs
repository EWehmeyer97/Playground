using System.Collections.Generic;
using UnityEngine;

public class InventorySpawn : MonoBehaviour
{
    [SerializeField] private Transform spawn;
    [SerializeField] private InventorySearcher searcher;
    [SerializeField] private InventoryItemDisplay displayElement;
    public void SpawnList(List<int> ids)
    {
        spawn.DestroyChildren();

        List<InventoryItemDisplay> displayItems = new List<InventoryItemDisplay>();
        foreach (int id in ids)
        {
            var item = Instantiate(displayElement, spawn);
            var material = MaterialInfo.Instance.GetMaterialItem(id);
            var inventory = InventoryInfo.Instance.GetInventoryItem(id);
            item.SetupDisplay(id, material.sprite, inventory.count, material.category == Category.Food && material.subcategory != SubCategory.Ingredient, inventory.isFavorite);
            displayItems.Add(item);
        }

        searcher.UpdateList(displayItems);
    }
}
