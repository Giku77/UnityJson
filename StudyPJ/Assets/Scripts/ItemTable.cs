using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string Id { get; set; }

    public string Name { get; set; }
    public int Value { get; set; }
    public int Price { get; set; }
    public string Des { get; set; }

}

public class ItemTable : DataTable
{
    public static readonly string ItemTableId = "Items";
    private static readonly string UnknownItemKey = "UNKNOWN_ITEM";


    private readonly Dictionary<string, (string, int, int, string)> _items = new Dictionary<string, (string, int, int, string)>();

    public override void Load(string fileName)
    {
        _items.Clear();
        var path = string.Format(dataTablePath, fileName);
        //var path = dataTablePath + fileName;
        var textAsset = Resources.Load<TextAsset>(path);
        if (textAsset == null)
        {
            Debug.LogError($"Failed to load string table: {fileName} at path: {path}");
            return;
        }
        var records = LoadCSV<ItemData>(textAsset.text);
        if (records == null || records.Count == 0)
        {
            Debug.LogWarning($"No records found in string table: {fileName}");
            return;
        }
        foreach (var record in records)
        {
            if (!_items.ContainsKey(record.Id))
            {
                _items.Add(record.Id, (record.Name, record.Value, record.Price, record.Des));
            }
            else
            {
                Debug.LogWarning($"Duplicate key found in string table: {record.Id}");
            }
        }
    }

    public (string, int, int, string) GetItem(string key)
    {
        if (_items.TryGetValue(key, out var value))
        {
            return value;
        }
        Debug.LogWarning($"Item key not found: {key}");
        return (UnknownItemKey, -1, -1, UnknownItemKey);
    }

}
