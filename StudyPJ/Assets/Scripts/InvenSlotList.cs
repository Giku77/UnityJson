using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine.Events;

public class InvenSlotList : MonoBehaviour
{

    public enum SortingType
    {
        CreationTimeAsc,
        CreationTimeDesc,
        NameAsc,
        NameDesc,
        CostAsc,
        CostDesc
    }

    public enum FilterType
    {
        None,
        Weapon,
        Equip,
        Consumable
    }

    public readonly System.Comparison<SaveItemData>[] Comparisons =
    {
        (a, b) => a.createTime.CompareTo(b.createTime), 
        (a, b) => b.createTime.CompareTo(a.createTime),
        (a, b) => string.Compare(a.itemdata.Name, b.itemdata.Name), 
        (a, b) => string.Compare(b.itemdata.Name, a.itemdata.Name), 
        (a, b) => a.itemdata.Cost.CompareTo(b.itemdata.Cost),
        (a, b) => b.itemdata.Cost.CompareTo(a.itemdata.Cost)  
    };

    public readonly System.Func<SaveItemData, bool>[] Filters =
    {
        (a) => true, 
        (a) => a.itemdata.Type == ItemTypes.Weapon, 
        (a) => a.itemdata.Type == ItemTypes.Equip, 
        (a) => a.itemdata.Type == ItemTypes.Consumable 
    };

    public ScrollRect scrollView;

    public InvenSlot invenSlotPrefab;

    private List<InvenSlot> listView = new List<InvenSlot>();

    private SortingType currentSorting = SortingType.NameAsc;
    private FilterType currentFilter = FilterType.None;
    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData>onSelectSlot;


    public SortingType CurrentSorting
    {
        get => currentSorting;
        set
        {
            if (currentSorting != value)
            {
                currentSorting = value;
                UpdateSlots(testItemList);
            }
        }
    }

    public FilterType CurrentFilter
    {
        get => currentFilter;
        set
        {
            if (currentFilter != value)
            {
                currentFilter = value;
                UpdateSlots(testItemList);
            }
        }
    }

    public int maxCount = 30;
    //private int itemCount = 0;

    private List<SaveItemData> testItemList = new List<SaveItemData>();

    private SaveItemData selectedItemData;

    private void Awake()
    {
        for (int i = 0; i < maxCount; i++)
        {
            var slot = Instantiate(invenSlotPrefab, scrollView.content);
            slot.slotIndex = i;
            var button = slot.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                selectedItemData = slot.itemData;
                Debug.Log($"Slot {slot.slotIndex} clicked.");
                selectedSlotIndex = slot.slotIndex;
                onSelectSlot?.Invoke(slot.itemData);
            });
            listView.Add(slot);
            slot.SetEmpty();
        }
    }        

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRandomItem();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log($"itemCount: {testItemList.Count}");
            //RemoveItem(0);
            RemoveItem();
            //if (itemCount > 0)
            //    RemoveItem(itemCount - 1);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Random Sort/Filter");
            CurrentSorting = (SortingType)Random.Range(0, 6);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            CurrentFilter = (FilterType)Random.Range(0, 4);
        }
    }

    private void OnEnable()
    {
        //Load();
        LoadData();
    }

    private void OnDisable()
    {
        //Save();
        SaveData();
    }

    private void UpdateSlots(List<SaveItemData> itemlist)
    {
        var list = itemlist.Where(Filters[(int)currentFilter]).ToList();
        list.Sort(Comparisons[(int)currentSorting]);


        if (listView.Count < list.Count)
        {
            int currentCount = listView.Count;
            int neededCount = list.Count - currentCount;
            for (int i = 0; i < neededCount; i++)
            {
                var slot = Instantiate(invenSlotPrefab, scrollView.content);
                //slot.slotIndex = currentCount + i;
                slot.slotIndex = listView.Count;

                var button = slot.GetComponent<Button>(); 
                button.onClick.AddListener(() => 
                {
                    selectedItemData = slot.itemData;
                    Debug.Log($"Slot {slot.slotIndex} clicked.");
                    selectedSlotIndex = slot.slotIndex;
                    onSelectSlot?.Invoke(slot.itemData);
                });

                listView.Add(slot);
                slot.SetEmpty();
            }
        }
        for(int i = 0; i < listView.Count; i++)
        {
            if (i < list.Count)
            {
                listView[i].SetItem(list[i]);
                listView[i].gameObject.SetActive(true);
            }
            else
            {
                listView[i].SetEmpty();
                listView[i].gameObject.SetActive(false);
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots?.Invoke();
        //int count = Mathf.Min(itemlist.Count, listView.Count);
        //for (int i = 0; i < count; i++)
        //{
        //    listView[i].SetItem(itemlist[i]);
        //    listView[i].gameObject.SetActive(true);
        //}
        //for (int i = count; i < listView.Count; i++)
        //{
        //    listView[i].SetEmpty();
        //    listView[i].gameObject.SetActive(false);
        //}
    }

    public void AddRandomItem()
    {
        //if (itemCount >= maxCount)
        //{
        //    maxCount += 10;
        //    for (int i = itemCount; i < maxCount; i++)
        //    {
        //        var slot = Instantiate(invenSlotPrefab, scrollView.content);
        //        slot.slotIndex = i;
        //        listView.Add(slot);
        //        slot.SetEmpty();
        //    }
        //}
        //var itemData = DataTableManger2.ItemTable.GetRandom();
        //listView[itemCount++].SetItem(itemData);
        var itemD = new SaveItemData();
        itemD.itemdata = DataTableManger2.ItemTable.GetRandom();
        testItemList.Add(itemD);
        UpdateSlots(testItemList);
    }
    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
            return;
        Debug.Log($"Remove Item at {selectedSlotIndex}");
        testItemList.Remove(selectedItemData);
        //testItemList.Remove(testItemList[selectedSlotIndex]);
        UpdateSlots(testItemList);
    }

    //public void Save()
    //{
    //    var json = JsonConvert.SerializeObject(testItemList, Formatting.Indented);
    //    var filePath = Path.Combine(Application.persistentDataPath, "test.json");
    //    File.WriteAllText(filePath, json);
    //}

    public void SaveData()
    {
        //SaveLoadManager.Data = new SaveDataV3();
        //SaveLoadManager.Data.Inventory = testItemList;
        //SaveLoadManager.Save(SaveLoadManager.SaveDataVersion - 1);
    }

    //public void Load()
    //{
    //    var filePath = Path.Combine(Application.persistentDataPath, "test.json");
    //    if (File.Exists(filePath))
    //    {
    //        var json = File.ReadAllText(filePath);
    //        testItemList = JsonConvert.DeserializeObject<List<SaveItemData>>(json);
    //        UpdateSlots(testItemList);
    //    }
    //}

    public void LoadData()
    {
        //SaveLoadManager.Data = new SaveDataV3();
        //if (SaveLoadManager.Load(SaveLoadManager.SaveDataVersion - 1))
        //{
        //    Debug.Log("Game Loaded");
        //    testItemList = SaveLoadManager.Data.Inventory ?? new List<SaveItemData>();
        //}
        //else
        //{
        //    Debug.Log("No Save Data");
        //}
        //UpdateSlots(testItemList);
    }


}
