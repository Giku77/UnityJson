using System.Collections.Generic;
using UnityEngine;

public static class DataTableManger2
{
    //private static readonly string ItemCsv = "Items";
    private static readonly Dictionary<string, DataTable> tables =
        new Dictionary<string, DataTable>();

    static DataTableManger2()
    {
        Init();
    }

    private static void Init()
    {
#if UNITY_EDITOR
        foreach (var id in DataTableIds.StringTableIds)
        {
            var table = new StringTable();
            table.Load(id);
            tables.Add(id, table);
        }
#else
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#endif

        var itemTable = new ItemTable2();
        //itemTable.Load(DataTableIds.Item);
        itemTable.Load("ItemTable");
        tables.Add(DataTableIds.Item, itemTable);
    }

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);
    public static ItemTable2 ItemTable => Get<ItemTable2>(DataTableIds.Item);

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
            return null;
        }
        return tables[id] as T;
    }
}