using UnityEngine;
using Newtonsoft.Json;
using System.IO;


[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int lives;
    public float health;
    public int[] inventoryItemIDs;
    public Vector3 position;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public override string ToString()
    {
        return $"Player Name: {playerName}, Lives: {lives}, Health: {health}, Inv: {string.Join(',', inventoryItemIDs)}";
    }
}

public class JsonUtilTest : MonoBehaviour
{
    private void Start()
    {
        PlayerData playerData = new PlayerData
        {
            playerName = "Hero",
            lives = 3,
            health = 75.5f,
            inventoryItemIDs = new int[] { 101, 102, 103 },
            position = Vector3.zero
        };

        var path = Application.persistentDataPath + "/playerdata.json";

        //string jsonString = playerData.SaveToString();
        string jsonString = JsonConvert.SerializeObject(playerData, Formatting.Indented, new Vector3Converter());
        File.WriteAllText(path, jsonString);
        Debug.Log("Serialized Player Data: " + jsonString);
        //PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonString);
        PlayerData loadedData = JsonConvert.DeserializeObject<PlayerData>(jsonString, new Vector3Converter());
        var readJson = File.ReadAllText(path);
        Debug.Log("Read JSON from file: " + readJson);
        Debug.Log("Deserialized Player Data: " + loadedData);
    }
}
