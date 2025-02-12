using System.Globalization;
using System.IO;
using UnityEngine;
using CsvHelper;
using System.Collections.Generic;
using System.ComponentModel;

public class InventoryInfo : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    public TextAsset csvData;

    [System.NonSerialized] public Dictionary<string, InventoryData> inventoryData = new Dictionary<string, InventoryData>();
    private void Awake()
    {
        using (var reader = new StreamReader(csvData.text))
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var item = new InventoryData
                    {
                        category = (Category)csv.GetField<int>("Category"),
                        subcategory = (SubCategory)(csv.GetField<int>("Category") * 3 + csv.GetField<int>("SubCategory")),
                        status = (Status)csv.GetField<int>("Status")
                    };
                    inventoryData.Add(csv.GetField("Name"), item);
                }
            }
        }
    }
}

public class InventoryData
{
    public Category category;
    public SubCategory subcategory;
    public Status status;
}

public enum Category
{
    [Description("Food")]
    Food,

    [Description("Monster Part")]
    Monster_Part,

    [Description("Material")]
    Material
}

public enum SubCategory
{
    [Description("Plant")] Plant, [Description("Meat")] Meat, [Description("Ingredient")] Ingredient,

    [Description("Eye")] Eye, [Description("Wing")] Wing, [Description("Skeletal")] Skeletal,

    [Description("Mineral")] Mineral, [Description("Bug")] Bug, [Description("Flower")] Flower
}

public enum Status
{
    Sharp,
    Blunt,
    Fire,
    Ice,
    Water,
    Electric
}