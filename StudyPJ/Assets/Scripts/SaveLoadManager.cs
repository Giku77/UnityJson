using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using SaveDataVC = SaveDataV4;

public class SaveLoadManager 
{
    public static int SaveDataVersion => 4;

    public static SaveDataVC Data { get; set; }

    private static readonly string[] SaveFileName =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json",
        "Save4.json",
        "Save5.json",
        "Save6.json",
        "Save7.json",
        "Save8.json",
        "Save9.json",
        "Save10.json"
    };


    public static string SaveDirectory => Application.persistentDataPath + "/Save";
    private static JsonSerializerSettings setiings => new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
    };


    public static bool Save(int slot = 0)
    {
        if (Data == null || slot < 0 || slot >= SaveFileName.Length)
        {
            Debug.LogError("No data to save!");
            return false;
        }

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
            var json = JsonConvert.SerializeObject(Data, setiings);
            File.WriteAllText(path, json);

            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save data: " + ex.Message);
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        if (slot < 0 || slot >= SaveFileName.Length)
        {
            Debug.LogError("Invalid slot number!");
            return false;
        }
        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
        if (!File.Exists(path))
        {
            Debug.LogWarning("No save file found at " + path);
            return false;
        }
        try
        {
            var json = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<SaveData>(json, setiings);

            while (data != null && data.Version < SaveDataVersion)
            {
                data = data.VersionUp();
            }
            Data = data as SaveDataVC;
            //Data = saveData as SaveDataV1;
            //Data = JsonConvert.DeserializeObject<SaveDataV1>(json, setiings); // null일경우 바로 예외처리됨

            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to load data: " + ex.Message);
            return false;
        }
    }
}
