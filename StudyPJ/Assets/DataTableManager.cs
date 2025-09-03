using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> _dataTables = new Dictionary<string, DataTable>();

    static DataTableManager()
    {
        Init();
    }

        private static void Init()
        {
            if (_dataTables.Count > 0)
                return;
            //var stringTable = new StringTable();
            //stringTable.Load(DataTableIds.StringTableIds[(int)Variables.CurrentLanguage]);
            //_dataTables.Add(DataTableIds.String, stringTable);

    #if UNITY_EDITOR
            foreach (var id in DataTableIds.StringTableIds)
            {
               var table = new StringTable();
               table.Load(id);
               _dataTables.Add(id, table);
            }
#else
            var t = new StringTable();
            t.Load(DataTableIds.StringTableIds[(int)Variables.CurrentLanguage]);
            _dataTables.Add(DataTableIds.String, t);
#endif
        var itemTable = new ItemTable();
        itemTable.Load(ItemTable.ItemTableId);
        _dataTables.Add(ItemTable.ItemTableId, itemTable);
    }

    public static StringTable GetStringTable()
    {
        return Get<StringTable>(DataTableIds.String);
    }

    public static ItemTable GetItemTable()
    {
        return Get<ItemTable>(ItemTable.ItemTableId);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!_dataTables.ContainsKey(id))
        {
            Debug.LogError($"DataTable with id {id} not found.");
            return null;
        }
        return _dataTables[id] as T;
        //Init();
        //var typeName = typeof(T).Name;
        //if (_dataTables.TryGetValue(typeName, out var table))
        //{
        //    return table as T;
        //}
        //Debug.LogError($"DataTable of type {typeName} not found.");
        //return null;
    }
}
