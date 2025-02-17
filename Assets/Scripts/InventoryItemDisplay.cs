using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    private int idRef;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;

    public void SetupDisplay(int id, Sprite itemSprite, int count)
    {
        idRef = id;
        itemImage.sprite = itemSprite;
        itemCount.text = "x" + count;
    }
}
