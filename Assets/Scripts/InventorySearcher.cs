using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySearcher : Singleton<InventorySearcher>
{
    [SerializeField] private RectTransform itemBar;
    [SerializeField] private Slider barSlider;
    [SerializeField] private TextMeshProUGUI itemName;

    [Space]

    [SerializeField] private float spacing = 172f;

    private List<InventoryItemDisplay> displayItems;
    private Vector2 originalPosition;
    private int trackedValue = 0;

    protected override void Awake()
    {
        base.Awake();

        originalPosition = itemBar.anchoredPosition;
        barSlider.onValueChanged.AddListener(UpdateSelected);
    }

    private void UpdateSelected(float value)
    {
        UpdateSelected(Mathf.RoundToInt(value));
    }

    public void UpdateSelected(int value)
    {
        trackedValue = value;

        itemName.text = MaterialInfo.Instance.GetMaterialItem(displayItems[value].ID).name;

        itemBar.anchoredPosition = originalPosition - Vector2.right * spacing * value;
    }

    public void CreateNewList(List<InventoryItemDisplay> newList)
    {
        displayItems = newList;
        barSlider.maxValue = displayItems.Count - 1;

        ResetPosition();
    }

    private void ResetPosition()
    {
        barSlider.SetValueWithoutNotify(0);
        itemBar.anchoredPosition = originalPosition;

        UpdateSelected(0);
    }
}
