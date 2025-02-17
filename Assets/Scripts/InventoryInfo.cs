using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class InventoryInfo : Singleton<InventoryInfo>
{
    [SerializeField] private TextAsset sampleSaveCSV;

    private Dictionary<int, InventoryData> inventoryData = new Dictionary<int, InventoryData>();

    public Dictionary<int, InventoryData> InventoryData {  get { return inventoryData; } }

    protected override void Awake()
    {
        base.Awake();

        LoadSaveData();
    }

    private void LoadSaveData()
    {
        using (var reader = new StringReader(sampleSaveCSV.text))
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var item = new InventoryData
                    {
                        count = csv.GetField<int>("Count"),
                        timesUsed = csv.GetField<int>("Times_Used"),
                        isFavorite = csv.GetField<int>("Is_Favorite") == 1
                    };
                    inventoryData.Add(csv.GetField<int>("ID"), item);
                }
            }
        }
    }
}

public class InventoryData
{
    public int count;
    public int timesUsed;
    public bool isFavorite;
}