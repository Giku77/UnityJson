using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvenSlot : MonoBehaviour
{
    public int slotIndex { get; set; }

    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveItemData itemData { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetEmpty();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var data = DataTableManger2.ItemTable.Get("Item1");
            //SetItem(data);
        }
    }

    public void SetEmpty()
    {
        itemData = null;

        imageIcon.sprite = null;
        imageIcon.enabled = false;
        textName.text = string.Empty;
    }

    public void SetItem(SaveItemData data)
    {
        itemData = data;

        imageIcon.sprite = data.itemdata.SpriteIcon;
        imageIcon.enabled = true;
        var s = data.itemdata.Name;
        var str = s.Replace("Item", "").Replace("Name", "").Trim();
        //Debug.Log(str);
        var nameIndex = int.Parse(str) - 1;
        textName.text = data.itemdata.StringName;
        //textName.text = DataTableIds.ItemTableIds2[nameIndex];
    }
}