using System.Globalization;
using System.IO;
using UnityEngine;
using CsvHelper;
using System.Collections.Generic;
using System.ComponentModel;

public class InventoryInfo : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    public TextAsset inventoryCSV;

    [System.NonSerialized] public Dictionary<int, InventoryData> inventoryData = new Dictionary<int, InventoryData>();
    private void Awake()
    {
        using (var reader = new StringReader(inventoryCSV.text))
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var item = new InventoryData
                    {
                        name = csv.GetField("Name"),
                        category = (Category)csv.GetField<int>("Category"),
                        subcategory = (SubCategory)(csv.GetField<int>("Category") * 3 + csv.GetField<int>("SubCategory")),
                        chemical = (Chemical)csv.GetField<int>("Chemical"),
                        attackPower = csv.GetField<int>("Attack_Power"),
                        isSharp = csv.GetField<int>("Is_Sharp") == 1
                    };
                    inventoryData.Add(csv.GetField<int>("ID"), item);
                }
            }
        }
    }
}

public class InventoryData
{
    public string name;
    public Category category;
    public SubCategory subcategory;
    public Chemical chemical;
    public int attackPower;
    public bool isSharp;
}

public enum Category
{
    [Description("Food")]
    Food,

    [Description("Monster Part")]
    Monster_Part,

    [Description("Material")]
    Material,

    [Description("Zonai Device")]
    Zonai_Device
}

public enum SubCategory
{
    [Description("Plant")] Plant, [Description("Meat")] Meat, [Description("Ingredient")] Ingredient,

    [Description("Eye")] Eye, [Description("Wing")] Wing, [Description("Body")] Body,

    [Description("Mineral")] Mineral, [Description("Creature")] Creature, [Description("Flower")] Flower,

    [Description("Tool")] Tool
}

public enum Chemical
{
    [Description("Normal")] Normal,
    [Description("Explosive")] Explosive, //1
    [Description("Fire")] Fire,
    [Description("Ice")] Ice,
    [Description("Water")] Water, //4
    [Description("Electric")] Electric,
    [Description("Wind")] Wind,
    [Description("Glow")] Glow //7
}