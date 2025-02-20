using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class SortingDisplayInventory : MonoBehaviour
{
    [SerializeField] private InventorySpawn spawner;
    [SerializeField] private TextMeshProUGUI sortingText;
    [SerializeField] private Button nextSort;
    [SerializeField] private Toggle[] toggles;
    [SerializeField] private string[] sortType;

    private int trackedValue = 0;

    void Awake()
    {
        //UI Navigation
        foreach(var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(SortList);
        }
        nextSort.onClick.AddListener(NextSort);
    }

    void OnEnable()
    {
        //Control Navigation
        InputActions.Instance.Input.Arrow_Fuse_UI.Sort.performed += NextSort;
    }

    void OnDisable()
    {
        //Control Navigation
        InputActions.Instance.Input.Arrow_Fuse_UI.Sort.performed -= NextSort;
    }

    private void Start()
    {
        SortList(true);
    }

    private void NextSort(InputAction.CallbackContext context)
    {
        NextSort();
    }

    private void NextSort()
    {
        trackedValue++;
        if (trackedValue == toggles.Length)
            trackedValue = 0;

        toggles[trackedValue].isOn = true;
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
