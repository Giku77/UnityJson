using Mono.Cecil;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationText))]
public class LocalizationTextEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    var text = target as LocalizationText;
    //    var newId = EditorGUILayout.TextField("String ID", text.stringId);
    //    var newLang = (Language)EditorGUILayout.EnumPopup("Language", text.language);

    //    if (newId != text.stringId || newLang != text.language)
    //    {
    //        //Undo.RecordObject(text, "Update Localization Text");
    //        text.stringId = newId;
    //        text.language = newLang;
    //        text.OnChangeLanguage(newLang);
    //        EditorUtility.SetDirty(text); // 변경 사항 저장
    //    }

    //    //DrawDefaultInspector();
    //    //LocalizationText locText = (LocalizationText)target;
    //    //if (GUILayout.Button("Update Text"))
    //    //{
    //    //    locText.OnChangeLanguage(locText.language);
    //    //    EditorUtility.SetDirty(locText); // 변경 사항 저장
    //    //}
    //}

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var pId = serializedObject.FindProperty("stringId");
        var pLan = serializedObject.FindProperty("language");

        EditorGUILayout.PropertyField(pId, new GUIContent("String ID"));
        EditorGUILayout.PropertyField(pLan, new GUIContent("Language"));

        if (serializedObject.ApplyModifiedProperties()) // 변경되었을 때만 호출
        {
            var comp = (LocalizationText)target;
            comp.OnChangeLanguage(comp.language);
            // 별도 SetDirty 불필요 (Undo/Prefab override 자동)
        }
    }


}
