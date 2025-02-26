using System.Globalization;
using System.IO;
using UnityEngine;
using CsvHelper;
using System.Collections.Generic;
using System.ComponentModel;
using System;

public class MaterialInfo : Singleton<MaterialInfo>
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private TextAsset inventoryCSV;

    private Dictionary<int, MaterialData> materialData = new Dictionary<int, MaterialData>();
    protected override void Awake()
    {
        base.Awake();

        SetupData();
    }

    private void SetupData()
    {
        using (var reader = new StringReader(inventoryCSV.text))
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var item = new MaterialData
                    {
                        name = csv.GetField("Name").Replace('_', ' '),
                        sprite = GetSprite(csv.GetField("Name")),
                        category = (MaterialCategory)csv.GetField<int>("Category"),
                        subcategory = (SubMaterialCategory)(csv.GetField<int>("Category") * 3 + csv.GetField<int>("SubCategory")),
                        chemical = (Chemical)csv.GetField<int>("Chemical"),
                        attackPower = csv.GetField<int>("Attack_Power"),
                        isSharp = csv.GetField<int>("Is_Sharp") == 1
                    };
                    materialData.Add(csv.GetField<int>("ID"), item);
                }
            }
        }
    }

    private Sprite GetSprite(string v)
    {
        foreach (var sprite in sprites)
            if (sprite.name.Equals(v))
                return sprite;
        return null;
    }

    public MaterialData GetMaterialItem(int id)
    {
        return materialData[id];
    }
}

public class MaterialData
{
    public Sprite sprite;
    public string name;
    public MaterialCategory category;
    public SubMaterialCategory subcategory;
    public Chemical chemical;
    public int attackPower;
    public bool isSharp;
}

public enum MaterialCategory
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

public enum SubMaterialCategory
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
    [Description("Ice")] Ice, //3
    [Description("Water")] Water,
    [Description("Electric")] Electric, //5
    [Description("Light")] Light
}