using UnityEngine;
using System.IO;
using CsvHelper;
using System.Globalization;

public class CsvData
{
    public string Id { get; set; }
    public string String { get; set; }

}

public class CsvTest : MonoBehaviour
{

    public TextAsset csvFile;
    private readonly string csvFilePath = "DataTables/Str_KR";

  
    private void Start()
    {
        TextAsset csv = Resources.Load<TextAsset>(csvFilePath); // 확장자는 제외하고 작성

        if (csv == null)
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다: " + csvFilePath);
            return;
        }

        using(var reader = new StringReader(csv.text))
        {
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<CsvData>();
                foreach(var record in records)
                {
                    Debug.Log($"Id: {record.Id}, String: {record.String}");
                }
            }
        }
        Resources.UnloadAsset(csv); // 언로드를 안하면 메모리에 계속 남아있음
        Debug.Log(csv.text);
    }

}
