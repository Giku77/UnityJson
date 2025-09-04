using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemValue;

    private void OnEnable()
    {
        SetEmpty();
    }


    public void SetEmpty()
    {
        icon.sprite = null;
        itemName.text = "None";
        itemDesc.text = "No Item Selected";
        itemType.text = "N/A";
        itemPrice.text = "0";
        itemValue.text = "0";
    }

    public void SetItem(SaveItemData data)
    {
        icon.sprite = data.itemdata.SpriteIcon;
        itemName.text = data.itemdata.StringName;
        itemDesc.text = data.itemdata.StringDesc;
        itemType.text = data.itemdata.Type.ToString();
        itemPrice.text = data.itemdata.Cost.ToString();
        itemValue.text = data.itemdata.Value.ToString();
    }
}
