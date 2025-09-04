using UnityEngine;

public enum Language
{
  Korean,
  English,
  Japanese,
}

public enum Icons
{
  Sword01,
  Bow01,
  Heart01,
}

public enum Items
{
  Sword,
  Bow,
  Heart,
}

public static class DataTableIds
{
  public static readonly string[] StringTableIds = {
        "StringTableKr",
        "Str_EN",
        "Str_JP"
    };

  public static readonly string[] ItemIconTableIds = {
        "icon/Icon_Sword01",
        "icon/Icon_Bow01",
        "icon/Icon_Heart01",
    };
  public static readonly string[] ItemTableIds = {
        "Sword",
        "Bow",
        "Heart",
    };

  public static readonly string[] ItemTableIds2 = {
        "검",
        "방패",
        "활",
        "하트",
    };



    public static string String => StringTableIds[(int)Variables.CurrentLanguage];
    public static string ItemIcon => ItemIconTableIds[(int)Variables.CurrentIcon];
    public static string Item => ItemTableIds[(int)Variables.CurrentIcon];
}

public static class Variables
{
  public static Language CurrentLanguage = Language.Korean;
  public static Icons CurrentIcon = Icons.Sword01;
}

