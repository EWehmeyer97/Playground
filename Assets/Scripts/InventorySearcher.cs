using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

public class InventorySearcher : Singleton<InventorySearcher>
{
    [Header("Moving Bar")]
    [SerializeField] private RectTransform itemBar;

    [Header("UI Elements")]
    [SerializeField] private Slider barSlider;
    [SerializeField] private Button leftPress;
    [SerializeField] private Button rightPress;
    [SerializeField] private TextMeshProUGUI itemName;

    [Space]

    [Header("Numeric Values")]
    [SerializeField] private float moveTime = 0.05f;
    [SerializeField] private float spacing = 172f;

    private List<InventoryItemDisplay> displayItems;
    private Vector2 originalPosition;
    private int trackedValue = 0;

    protected override void Awake()
    {
        base.Awake();

        originalPosition = itemBar.anchoredPosition;
    }

    private void Start()
    {
        //UI Controls
        barSlider.onValueChanged.AddListener(UpdateSelected);
        leftPress.onClick.AddListener(UpdateLeft);
        rightPress.onClick.AddListener(UpdateRight);

        //Input Controls
        InputActions.Instance.EnableFuseUI();
        InputActions.Instance.Input.Arrow_Fuse_UI.Search.performed += UpdateControl;
    }

    private void UpdateControl(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0)
            UpdateLeft();
        else
            UpdateRight();
    }

    private void UpdateSelected(float value)
    {
        UpdateSelected(Mathf.RoundToInt(value));
    }

    public void UpdateSelected(int value)
    {
        if (displayItems.Count == 0)
            value = 0;
        else if (value < 0)
            value = displayItems.Count;
        else if (value > displayItems.Count)
            value = 0;

        trackedValue = value;

        itemName.text = value == 0 ? "" : MaterialInfo.Instance.GetMaterialItem(displayItems[(value - 1)].ID).name;
        barSlider.SetValueWithoutNotify(value);

        itemBar.DOAnchorPosX(originalPosition.x - spacing * value, moveTime);
    }

    private void UpdateLeft()
    {
        UpdateSelected(trackedValue - 1);
    }

    private void UpdateRight()
    {
        UpdateSelected(trackedValue + 1);
    }

    public void CreateNewList(List<InventoryItemDisplay> newList)
    {
        displayItems = newList;
        barSlider.maxValue = displayItems.Count;

        ResetPosition();
    }

    private void ResetPosition()
    {
        int x = displayItems.Count == 0 ? 0 : 1;
        barSlider.SetValueWithoutNotify(x);

        UpdateSelected(x);
    }
}
