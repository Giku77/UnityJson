using UnityEngine;

public enum Language
{
  Korean,
  English,
  Japanese,
}

public static class DataTableIds
{
  public static readonly string[] StringTableIds = { 
        "Str_KR",
        "Str_EN",
        "Str_JP"
    };


    public static string String => StringTableIds[(int)Variables.CurrentLanguage];
}

public static class Variables
{
  public static Language CurrentLanguage = Language.Korean;
}

