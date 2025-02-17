using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SortingDisplayInventory : Singleton<SortingDisplayInventory>
{
    [SerializeField] private InventorySpawn spawner;
    [SerializeField] private TextMeshProUGUI sortingText;
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private string[] sortType;

    protected override void Awake()
    {
        base.Awake();

        foreach(var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(SortList);
        }
    }

    private void Start()
    {
        SortList(true);
    }

    private void SortList(bool arg)
    {
        if (!arg)
            return;

        int sortIndex;
        for (sortIndex = 0; sortIndex < toggles.Length; sortIndex++)
            if (toggles[sortIndex].isOn)
                break;

        spawner.SpawnList(SpawnableList(sortIndex));
        sortingText.text = sortType[sortIndex];
    }

    private List<int> SpawnableList(int sortIndex)
    {
        var list = new List<int>();
        switch (sortIndex)
        {
            case 0:
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.OrderBy(item => MaterialInfo.Instance.GetMaterialItem(item).category));
                break;
            case 1:
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.OrderByDescending(item => MaterialInfo.Instance.GetMaterialItem(item).attackPower));
                break;
            case 2:
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.OrderByDescending(item => InventoryInfo.Instance.GetInventoryItem(item).timesUsed));
                break;
            case 3:
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.Where(item => MaterialInfo.Instance.GetMaterialItem(item).category == Category.Zonai_Device));
                break;
            default:
                break;
        }

        return list;
    }
}
