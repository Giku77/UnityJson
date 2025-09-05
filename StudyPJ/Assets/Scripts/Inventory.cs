using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public InvenSlotList invenSlotList;

    public void OnEnable()
    {
        sorting.value = (int)invenSlotList.CurrentSorting;
        filtering.value = (int)invenSlotList.CurrentFilter;
    }


    public void OnChangeSorting(int index)
    {
        invenSlotList.CurrentSorting = (InvenSlotList.SortingType)index;
    }

    public void OnChangeFiltering(int index)
    {
        invenSlotList.CurrentFilter = (InvenSlotList.FilterType)index;
    }

}
