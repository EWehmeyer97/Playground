using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    private int idRef;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private GameObject heartIcon;
    [SerializeField] private GameObject favoriteIcon;

    public void SetupDisplay(int id, Sprite itemSprite, int count, bool isFood, bool isFavorite)
    {
        idRef = id;
        itemImage.sprite = itemSprite;
        itemCount.text = "x" + count;

        heartIcon.SetActive(isFood);
        favoriteIcon.SetActive(isFavorite);

    }

    public int ID { get { return idRef; } }
}
