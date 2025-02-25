using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CategoryDisplayInventory : TogglePaginationMenu
{
    [SerializeField] private Toggle favoritesOnly;
    [SerializeField] private SortingDisplayInventory sortInventory;
    [SerializeField] private GameObject subCategoryMenu;

    private bool onlyFavorite = false;

    protected override void Awake()
    {
        base.Awake();

        favoritesOnly.onValueChanged.AddListener(OnFavorite);
    }

    void OnEnable()
    {
        //Control Navigation
        InputActions.Instance.Input.Arrow_Fuse_UI.MenuSearch.performed += Next;
        InputActions.Instance.Input.Arrow_Fuse_UI.FavoriteToggle.performed += OnFavorite;
    }

    void OnDisable()
    {
        //Control Navigation
        InputActions.Instance.Input.Arrow_Fuse_UI.MenuSearch.performed -= Next;
        InputActions.Instance.Input.Arrow_Fuse_UI.FavoriteToggle.performed -= OnFavorite;
    }

    private void Next(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0f)
            Back();
        else
            Next();
    }

    private void OnFavorite(InputAction.CallbackContext context)
    {
        OnFavorite(!onlyFavorite);
    }

    private void OnFavorite(bool arg0)
    {
        onlyFavorite = arg0;
        favoritesOnly.SetIsOnWithoutNotify(arg0);
        Sort(true);
    }

    public override void Sort(bool arg)
    {
        base.Sort(arg);

        sortInventory.Sort(arg);
    }

    public List<int> GetCategory()
    {
        var list = new List<int>();

        switch (TrackedValue)
        {
            case 0: //All
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys);
                break;
            case 1: //Food
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.Where(item => MaterialInfo.Instance.GetMaterialItem(item).category == Category.Food));
                break;
            case 2: //Monster
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.Where(item => MaterialInfo.Instance.GetMaterialItem(item).category == Category.Monster_Part));
                break;
            case 3: //Material
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.Where(item => MaterialInfo.Instance.GetMaterialItem(item).category == Category.Material));
                break;
            case 4: //Zonai
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.Where(item => MaterialInfo.Instance.GetMaterialItem(item).category == Category.Zonai_Device));
                break;
            case 5: //Elemental
                list.AddRange(InventoryInfo.Instance.InventoryData.Keys.Where(item => MaterialInfo.Instance.GetMaterialItem(item).chemical != Chemical.Normal));
                break;
        }

        if (onlyFavorite)
            list = list.Where(item => InventoryInfo.Instance.GetInventoryItem(item).isFavorite == true).ToList<int>();

        return list;
    }
}
