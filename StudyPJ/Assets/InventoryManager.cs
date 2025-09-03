using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemslotPrefab;

    public Image img;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Value;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Des;

    private ItemTable itemTable;


    private void Start()
    {
        itemTable = new ItemTable();
        itemTable.Load(ItemTable.ItemTableId);
        //Debug.Log(itemTable.GetItem("Sword"));
        //Debug.Log(itemTable.GetItem("Bow"));
        //Debug.Log(itemTable.GetItem("Heart"));
    }

    public void onAddItemButton()
    {
        var prefab = Instantiate(itemslotPrefab, itemslotPrefab.transform);
        var prefabimg = prefab.GetComponentsInChildren<Image>()[1];
        if (prefabimg != null)
         prefabimg.sprite = Resources.Load<Sprite>("icon/Icon_Sword01");

        var item = itemTable.GetItem("Sword");

        img.sprite = Resources.Load<Sprite>("icon/Icon_Sword01");
        Name.text = item.Item1;
        Value.text = item.Item2.ToString();
        Price.text = item.Item3.ToString();
        Des.text = item.Item4;
    }
}
