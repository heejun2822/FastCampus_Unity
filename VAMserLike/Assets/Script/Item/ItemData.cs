using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EItemType
{
    None,
    Exp,
    Weapon,
}

public class ItemData
{
    public string Id = "";
    public string Path = "";
    public EItemType Type = EItemType.None;
    public int Value = 0;

    public bool IsValid()
    {
        return Id != string.Empty;
    }
    public string ShowItemDataLog()
    {
        return ("ItemId : " + Id + " / Type : " + Type.ToString());
    }
}
