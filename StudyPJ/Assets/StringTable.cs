using System;
using System.Collections.Generic;
using UnityEngine;

public class StringTable : DataTable
{
    private static readonly string UnknownStringKey = "UNKNOWN_STRING_KEY";
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }


    private readonly Dictionary<string, string> _strings = new Dictionary<string, string>(); // readonly : GC에서 제외

    public override void Load(string fileName)
    {
        _strings.Clear();
        var path = string.Format(dataTablePath, fileName);
        //var path = dataTablePath + fileName;
        var textAsset = Resources.Load<TextAsset>(path);
        if (textAsset == null)
        {
            Debug.LogError($"Failed to load string table: {fileName} at path: {path}");
            return;
        }
        var records = LoadCSV<Data>(textAsset.text);
        if (records == null || records.Count == 0)
        {
            Debug.LogWarning($"No records found in string table: {fileName}");
            return;
        }
        foreach (var record in records)
        {
            //Debug.Log($"Id: {record.Id}, String: {record.String}");
            if (!_strings.ContainsKey(record.Id))
            {
                _strings.Add(record.Id, record.String);
            }
            else
            {
                Debug.LogWarning($"Duplicate key found in string table: {record.Id}");
            }
        }
        //var records = LoadCSV<(string, string)>(textAsset.text);
        //foreach (var (key, value) in records)
        //{
        //    if (!_strings.ContainsKey(key))
        //    {
        //        _strings.Add(key, value);
        //    }
        //    else
        //    {
        //        Debug.LogWarning($"Duplicate key found in string table: {key}");
        //    }
        //}
        //단순히 Key-Value 딱 두 개만 쓰고, 절대 확장할 일 없다 = 튜플로 해도 됨.
        //가독성·확장성·파서 호환성을 생각하면 = Data 클래스가 더 안정적인 선택.
    }

    public string GetString(string key)
    {
        if (_strings.TryGetValue(key, out var value))
        {
            return value;
        }
        Debug.LogWarning($"String key not found: {key}");
        return UnknownStringKey; 
    }
}
