using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemslotPrefab;

    public Transform itemSlotParent;

    public ScrollRect scrollRect;       
    public RectTransform content;      

    public Image img;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Value;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Des;

    private ItemTable itemTable;
    private Button slotButton;


    private void Start()
    {
        //itemTable = new ItemTable();
        //itemTable.Load(ItemTable.ItemTableId);

        if (scrollRect != null && content != null && scrollRect.content != content)
            scrollRect.content = content;
        //Debug.Log(itemTable.GetItem("Sword"));
        //Debug.Log(itemTable.GetItem("Bow"));
        //Debug.Log(itemTable.GetItem("Heart"));
    }

    public void onAddItemButton()
    {
        var slotGO = Instantiate(itemslotPrefab, content);

        var prefabimg = slotGO.GetComponentsInChildren<Image>()[1];
        var icon = Enum.GetValues(typeof(Icons));
        int iconIndex = UnityEngine.Random.Range(0, icon.Length);

        var slotItem = slotGO.GetComponent<ItemSlot>();
        if (slotItem != null)
            slotItem.Init(this, DataTableIds.Item, Resources.Load<Sprite>(DataTableIds.ItemIcon));
        slotButton = slotGO.GetComponent<Button>();
        if (slotButton != null)
            slotButton.onClick.AddListener(() => onSlotItemButton(slotItem));
        slotGO.transform.SetAsLastSibling(); //부모의 자식들 중에서 맨 마지막에 추가

        Variables.CurrentIcon = (Icons)icon.GetValue(iconIndex);
        if (prefabimg != null)
            prefabimg.sprite = slotItem.icon;

        Canvas.ForceUpdateCanvases();
        Debug.Log("Content Height: " + content.sizeDelta.y);
        scrollRect.verticalNormalizedPosition = 0f; // 0 = 맨 아래, 1 = 맨 위
    }

    public void onSlotItemButton(ItemSlot slotitem)
    {
        //var item = itemTable.GetItem("Sword");
        //var item = DataTableManager.GetItemTable().GetItem(DataTableIds.Item);
        var item = DataTableManager.GetItemTable().GetItem(slotitem.itemKey);  
        //img.sprite = Resources.Load<Sprite>(DataTableIds.ItemIcon);
        img.sprite = slotitem.icon;
        Name.text = item.Item1;
        Value.text = item.Item2.ToString();
        Price.text = item.Item3.ToString();
        Des.text = item.Item4;
    }
}
