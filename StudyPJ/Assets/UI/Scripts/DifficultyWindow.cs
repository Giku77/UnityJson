using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    public int index;

    public ToggleGroup toggleGroup;

    public Toggle[] toggles;

    private void OnDisable()
    {
        SaveLoadManager.Data = new SaveDataV4();
        SaveLoadManager.Data.ActiveIndex = index;
        SaveLoadManager.Save();
    }


    public override void Open()
    {
        base.Open();
        //SaveLoadManager.Data = new SaveDataV4();
        //if (SaveLoadManager.Load())
        //{
        //    index = SaveLoadManager.Data.ActiveIndex;
        //}
        index = SaveLoadManager.Data.ActiveIndex;
        toggles[index].isOn = true;
    }

    public void OnToggle()
    {
        foreach (var t in toggles)
        {
            if (t.isOn)
            {
                //index = t.transform.GetSiblingIndex();
                Debug.Log("index: " + index);
                break;
            }
        }
    }

    public void OnclickEasy(bool v)
    {
        if (v == false) return;
        index = 0;
        Debug.Log("Easy");
    }

    public void OnclickNormal(bool v)
    {
        if (v == false) return;
        index = 1;
        Debug.Log("Normal");
    }

    public void OnclickHard(bool v)
    {
        if (v == false) return;
        index = 2;
        Debug.Log("Hard");
    }
}
