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


    private readonly Dictionary<string, string> _strings = new Dictionary<string, string>(); // readonly : GC���� ����

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
        //�ܼ��� Key-Value �� �� ���� ����, ���� Ȯ���� �� ���� = Ʃ�÷� �ص� ��.
        //��������Ȯ�强���ļ� ȣȯ���� �����ϸ� = Data Ŭ������ �� �������� ����.
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
