using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitData
{
    public int TotalHp = 0;
    public int Hp = 0;
    public int Power = 0;
    public int Armor = 0;
}

public class UnitBase : MonoBehaviour
{
    public bool mIsAlive { private set; get; }
    public UnitData mUnitData { private set; get; }
    public int mUnitId { private set; get; }

    void Start()
    {
        
    }

    public virtual void InitUnit(int InUnitId, int InHp, int InPower, int InArmor)
    {
        mUnitId = InUnitId;
        mUnitData = new UnitData();
        mUnitData.TotalHp = mUnitData.Hp = InHp;
        mUnitData.Power = InPower;
        mUnitData.Armor = InArmor;
    }

    public virtual void OnHit(int InDamage)
    {
        if (mUnitData != null)
        {
            return;
        }
    }
    public virtual void OnDie()
    {
        
    }
}
