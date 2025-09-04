using System.Collections.Generic;
using CsvHelper;
using UnityEngine;

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable,
}

public class ItemData2
{
    public string Id { get; set; }
    public ItemTypes Type { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }
    public string Icon { get; set; }

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name} / {Desc} / {Value} / {Cost} / {Icon}";
    }

    public string StringName => DataTableManger2.StringTable.GetString(Name);
    public string StringDesc => DataTableManger2.StringTable.GetString(Desc);

    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
}

public class ItemTable2 : DataTable
{
    private readonly Dictionary<string, ItemData2> table = new Dictionary<string, ItemData2>();

    public override void Load(string filename)
    {
        table.Clear();

        var path = string.Format(dataTablePath, filename);
        Debug.Log(path);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<ItemData2>(textAsset.text);
        foreach (var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("아이템 아이디 중복");
            }
        }
    }

    public ItemData2 Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            return null;
        }
        return table[id];
    }

    public ItemData2 GetRandom()
    {
        var values = new List<ItemData2>(table.Values);
        int index = Random.Range(0, values.Count);
        return values[index];
    }
}