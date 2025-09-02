using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class CubeSaveLoad : MonoBehaviour
{
    public RandomSpawnCube randomSpawnCube;

    public GameObject[] prefabs;

    public CubeData[] cubeData;
    private static string filePath => Path.Combine(Application.persistentDataPath, "cubeData.json");


    public void SaveCubeData()
    {
        //if (randomSpawnCube.cubeData == null) {
        //    Debug.LogWarning("No cube data to save. Please spawn cubes first.");
        //    return;
        //}
        //var settings = new JsonSerializerSettings
        //{
        //    Converters = { new Vector3Converter(), new ColorConverter() }
        //};
        //for (int i = 0; i < randomSpawnCube.cubeData.Length; i++)
        //{
        //    string json = JsonConvert.SerializeObject(randomSpawnCube.cubeData, Formatting.Indented, settings);
        //    File.WriteAllText(filePath, json);
        //}
        var existingCubes = GameObject.FindGameObjectsWithTag("Cube");
        cubeData = new CubeData[existingCubes.Length];
        for (int i = 0; i < existingCubes.Length; i++)
        {
            var cubeTransform = existingCubes[i].transform;
            var renderer = existingCubes[i].GetComponent<Renderer>();
            Color color = Color.white;
            if (renderer != null)
            {
                color = renderer.material.color;
            }
            var data = new CubeData(cubeTransform.position, cubeTransform.localScale, cubeTransform.rotation, color);
            var typeName = existingCubes[i].name.Replace("(Clone)", "").Trim();
            data.Type = (CubeData.CubeType)System.Enum.Parse(typeof(CubeData.CubeType), typeName);
            cubeData[i] = data;
        }
        var settings = new JsonSerializerSettings
        {
            Converters = { new Vector3Converter(), new ColorConverter() }
        };
        string json = JsonConvert.SerializeObject(cubeData, Formatting.Indented, settings);
        File.WriteAllText(filePath, json);
    }

    public void LoadCubeData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"File not found: {filePath}");
            return;
        }
        string json = File.ReadAllText(filePath);
        var settings = new JsonSerializerSettings
        {
            Converters = { new Vector3Converter(), new ColorConverter() }
        };

        var cubeData = JsonConvert.DeserializeObject<CubeData[]>(json, settings);
        //var cubeData = JsonConvert.DeserializeObject<CubeData[]>(json, new Vector3Converter(), new ColorConverter());
        for (int i = 0; i < cubeData.Length; i++)
        {
            if (cubeData[i] != null)
            {
                //Instantiate(cube, cubeData[i].Position, Quaternion.Euler(cubeData[i].Rotation)).transform.localScale = cubeData[i].Scale;
                var prefab = prefabs[(int)cubeData[i].Type];
                var newCube = Instantiate(prefab, cubeData[i].Position, Quaternion.Euler(cubeData[i].Rotation));
                newCube.transform.localScale = cubeData[i].Scale;
                var renderer = newCube.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = cubeData[i].Color;
                }
            }
        }
    }

    public void Clear()
    {
        //File.Delete(filePath);
        var existingCubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach (var cube in existingCubes)
        {
            Destroy(cube);
        }
    }

}
