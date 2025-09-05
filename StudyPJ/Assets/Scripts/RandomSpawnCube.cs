using NUnit.Framework;
using UnityEngine;

public class RandomSpawnCube : MonoBehaviour
{
    public GameObject[] Prefabs;

    public CubeData[] cubeData;
    private int numberOfCubes;

    public void SpawnButtonClicked()
    {
        numberOfCubes = Random.Range(10, 40);
        cubeData = new CubeData[numberOfCubes];
        for (int i = 0; i < numberOfCubes; i++)
        {
            if (Prefabs == null)
            {
                Debug.LogError("Cube Prefab is not assigned.");
                return;
            }
            Vector3 randomPosition = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            );
            Vector3 radomSclae = new Vector3(
                Random.Range(0.1f, 3f),
                Random.Range(0.1f, 3f),
                Random.Range(0.1f, 3f)
            );
            Quaternion randomRotate = Quaternion.Euler(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );
            Color randomColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
            //cubeData[i] = new CubeData(randomPosition, radomSclae, randomRotate);
            cubeData[i] = new CubeData(randomPosition, radomSclae, randomRotate, randomColor);
            var Prefab = Prefabs[Random.Range(0, Prefabs.Length)];
            var cube = Instantiate(Prefab, randomPosition, randomRotate);
            cube.transform.localScale = radomSclae;
            var renderer = cube.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = randomColor;
            }
            //Instantiate(cubePrefab, randomPosition, randomRotate).transform.localScale = radomSclae;
        }
    }
}
