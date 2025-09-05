using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [HideInInspector] public string itemKey;      
    [HideInInspector] public Sprite icon;         
    private InventoryManager owner;               
    [SerializeField] private Button button;       
    //[SerializeField] private Image iconImage;    

    public void Init(InventoryManager owner, string itemKey, Sprite icon)
    {
        this.owner = owner;
        this.itemKey = itemKey;
        this.icon = icon;
        //if (iconImage != null) iconImage.sprite = icon;

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClicked);
        }
    }

    private void OnClicked()
    {
        owner.onSlotItemButton(this); // ´­¸° ½½·Ô ÀÚ½ÅÀ» ³Ñ±è
    }
}