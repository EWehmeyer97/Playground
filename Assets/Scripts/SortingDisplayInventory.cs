using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class SortingDisplayInventory : TogglePaginationMenu
{
    [SerializeField] private InventorySpawn spawner;
    [SerializeField] private CategoryDisplayInventory category;
    [SerializeField] private TextMeshProUGUI sortingText;
    [SerializeField] private string[] sortType;

    void OnEnable()
    {
        //Control Navigation
        InputActions.Instance.Input.Arrow_Fuse_UI.Sort.performed += Next;
    }

    void OnDisable()
    {
        //Control Navigation
        InputActions.Instance.Input.Arrow_Fuse_UI.Sort.performed -= Next;
    }

    private void Start()
    {
        Sort(true);
    }

    private void Next(InputAction.CallbackContext context)
    {
        Next();
    }

    public override void Sort(bool arg)
    {
        base.Sort(arg);

        spawner.SpawnList(SpawnableList());
        sortingText.text = sortType[TrackedValue];
    }

    private List<int> SpawnableList()
    {
        var list = new List<int>();

        var keys = category == null ? InventoryInfo.Instance.InventoryData.Keys.ToList<int>() : category.GetCategory();
        switch (TrackedValue)
        {
            case 0:
                list.AddRange(keys.OrderBy(item => MaterialInfo.Instance.GetMaterialItem(item).category));
                break;
            case 1:
                list.AddRange(keys.OrderByDescending(item => MaterialInfo.Instance.GetMaterialItem(item).attackPower));
                break;
            case 2:
                list.AddRange(keys.OrderByDescending(item => InventoryInfo.Instance.GetInventoryItem(item).timesUsed));
                break;
            case 3:
                list.AddRange(keys.Where(item => MaterialInfo.Instance.GetMaterialItem(item).category == Category.Zonai_Device));
                break;
            default:
                break;
        }

        return list;
    }
}
