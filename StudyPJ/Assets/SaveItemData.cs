using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class SaveItemData
{
    public Guid instanceId;


    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData2 itemdata;

    public DateTime createTime;

    public SaveItemData()
    {
        instanceId = Guid.NewGuid();
        createTime = DateTime.Now;
    }
}
