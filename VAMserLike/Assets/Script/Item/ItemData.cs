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

public class DropDataInfo
{
    public string ItemId;
    public int DropRatio;

    public int DropSelectMinValue;
    public int DropSelectMaxValue;
}

public class DropData
{
    public List<DropDataInfo> DropList;
    public void PostLoad()
    {
        foreach (var EachDrop in DropList)
        {
            EachDrop.DropSelectMinValue = mDropMaxValue;
            mDropMaxValue += EachDrop.DropRatio;
            EachDrop.DropSelectMaxValue = mDropMaxValue - 1;
        }
    }
    public DropDataInfo RandomPickDropInfo()
    {
        int IRandomValue = Random.Range(0, mDropMaxValue);
        foreach (var EachDrop in DropList)
        {
            if (IRandomValue >= EachDrop.DropSelectMinValue && IRandomValue <= EachDrop.DropSelectMaxValue)
            {
                return EachDrop;
            }
        }
        return null;
    }

    private int mDropMaxValue = 0;
}
