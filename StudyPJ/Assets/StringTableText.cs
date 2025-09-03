using TMPro;
using UnityEngine;

public class StringTableText : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        var stringTableManager = DataTableManager.Get<StringTable>("String");
        textMeshPro.text = stringTableManager.GetString(id);
    }
}
