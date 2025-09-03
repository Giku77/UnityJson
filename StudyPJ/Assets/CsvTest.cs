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
        TextAsset csv = Resources.Load<TextAsset>(csvFilePath); // Ȯ���ڴ� �����ϰ� �ۼ�

        if (csv == null)
        {
            Debug.LogError("CSV ������ ã�� �� �����ϴ�: " + csvFilePath);
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
        Resources.UnloadAsset(csv); // ��ε带 ���ϸ� �޸𸮿� ��� ��������
        Debug.Log(csv.text);
    }

}
