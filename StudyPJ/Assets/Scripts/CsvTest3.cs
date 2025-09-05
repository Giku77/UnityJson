using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CsvTest3 : MonoBehaviour
{
    public Image img;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Value;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Des;



    private void Start()
    {
        var itemTable = new ItemTable();
        itemTable.Load(ItemTable.ItemTableId);

        var item = itemTable.GetItem("Sword");

        img.sprite = Resources.Load<Sprite>("icon/Icon_Sword01");
        Name.text = item.Item1;
        Value.text = item.Item2.ToString();
        Price.text = item.Item3.ToString();
        Des.text = item.Item4;

        //Debug.Log(itemTable.GetItem("Sword"));
        //Debug.Log(itemTable.GetItem("Bow"));
        //Debug.Log(itemTable.GetItem("Heart"));
    }
}
