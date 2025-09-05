using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }

    public abstract SaveData VersionUp();
    
}


[Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;
    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var saveDataV2 = new SaveDataV2
        {
            Name = PlayerName,
            Gold = 0 
        };
        return saveDataV2;
    }
}

public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold;
    public SaveDataV2()
    {
        Version = 2;
    }
    public override SaveData VersionUp()
    {
        var saveDataV3 = new SaveDataV3();
        saveDataV3.Name = Name;
        saveDataV3.Gold = Gold;
        return saveDataV3;
    }
}

public class SaveDataV3 : SaveData
{
    public List<SaveItemData> Inventory = new List<SaveItemData>();
    public string Name { get; set; } = string.Empty;
    public int Gold;
    public SaveDataV3()
    {
        Version = 3;
    }
    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}
