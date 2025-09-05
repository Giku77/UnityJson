using TMPro;
using UnityEngine;


[ExecuteInEditMode] // ��Ÿ���� �ƴ϶� ����Ʈ�ϴ��߿��� ���� 
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizationText : MonoBehaviour
{
    public string stringId;
#if UNITY_EDITOR // ��ó�� ������, �����Ϳ����� �۵�
    public Language language;
#endif


    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            OnChangeLanguage();
        }
        else
        {
            OnChangeLanguage(language);
        }
#else
        OnChangeLanguage();
#endif
    }

    public void OnChangeLanguage()
    {
        var stringTable = DataTableManager.GetStringTable();
        _textMeshPro.text = stringTable.GetString(stringId);
    }

    public void OnChangeLanguage(Language lang)
    {
        if (_textMeshPro == null)
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        var tableId = DataTableIds.StringTableIds[(int)lang];
        var strTable = DataTableManager.Get<StringTable>(tableId);
        //strTable.Load(tableId);
        _textMeshPro.text = strTable.GetString(stringId);
    }

}
