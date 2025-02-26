using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SubCategoryDisplayInventory : TogglePaginationMenu
{
    [SerializeField] private SortingDisplayInventory inventory;

    private bool onElemental = false;
    private MaterialCategory category;

    void OnEnable()
    {
        //Control Navigation
        InputActions.Instance.Input.Arrow_Fuse_UI.SubMenuSearch.performed += Next;
    }

    void OnDisable()
    {
        //Control Navigation
        InputActions.Instance.Input.Arrow_Fuse_UI.SubMenuSearch.performed -= Next;
    }

    private void Next(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0f)
            Back();
        else
            Next();
    }

    public override void Sort(bool arg)
    {
        base.Sort(arg);

        inventory.Sort(arg);
    }

    //this method is hokey and requires programmer rewrite if Material Categories or Chemical list changes - should be reworked!
    public void UpdateOptions(int value)
    {
        onElemental = value == 4;
        if(!onElemental)
            category = (MaterialCategory)value;
        if (onElemental)
        {
            ActivateToggles(new string[] { "All", "Explosive", "Fire", "Ice", "Water", "Electric", "Light"});
        } else
        {
            switch (category)
            {
                case MaterialCategory.Food:
                    ActivateToggles(new string[] { "All", "Fruit", "Meat", "Ingredient" });
                    break;
                case MaterialCategory.Monster_Part:
                    ActivateToggles(new string[] { "All", "Eye", "Wing", "Skeletal", "Guts" });
                    break;
                case MaterialCategory.Material:
                    ActivateToggles(new string[] { "All", "Mineral", "Creature", "Flower" });
                    break;
            }
        }
        ResetValue();
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }


    private void ActivateToggles(string[] names)
    {
        int x = names.Length;
        toggles = new Toggle[x];
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i < x);
            if (i < x)
            {
                toggles[i] = transform.GetChild(i).GetComponent<Toggle>();
                toggles[i].targetGraphic.GetComponent<TextMeshProUGUI>().text = names[i];
                toggles[i].graphic.GetComponent<TextMeshProUGUI>().text = names[i];
            }
        }
    }

    internal List<int> GetSubCategory(List<int> list, int trackedValue)
    {
        if (TrackedValue == 0)
            return list;

        var newList = new List<int>();
        if (!onElemental)
            newList.AddRange(list.Where(item => (int)(MaterialInfo.Instance.GetMaterialItem(item).subcategory) == 3 * (int)category + (TrackedValue - 1)));
        else
            newList.AddRange(list.Where(item => (int)(MaterialInfo.Instance.GetMaterialItem(item).chemical) == TrackedValue));

        return newList;
    }
}
