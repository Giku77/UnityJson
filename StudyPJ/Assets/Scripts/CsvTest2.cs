using UnityEngine;

public class CsvTest2 : MonoBehaviour
{
    private void Start()
    {
        var strTable = new StringTable();
        strTable.Load("Str_KR");



        Debug.Log(strTable.GetString("H"));
        Debug.Log(strTable.GetString("B"));
        Debug.Log(strTable.GetString("D"));
    }
}
