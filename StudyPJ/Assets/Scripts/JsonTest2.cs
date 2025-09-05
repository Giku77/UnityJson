using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonTest2 : MonoBehaviour
{
    public static readonly string SaveFileName = "cube.json";
    public static string SaveFilePath => Path.Combine(Application.persistentDataPath, SaveFileName);

    public GameObject target;

    public void Save()
    {
        var json = JsonConvert.SerializeObject(target.transform.position, Formatting.Indented, new Vector3Converter());
        File.WriteAllText(SaveFilePath, json);
    }

    public void Load()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.LogError($"File not found: {SaveFilePath}");
            return;
        }
        var json = File.ReadAllText(SaveFilePath);
        //var position = JsonConvert.DeserializeObject<Vector3>(json);
        var position = JsonConvert.DeserializeObject<Vector3>(json, new Vector3Converter());
        target.transform.position = position;
    }

}
