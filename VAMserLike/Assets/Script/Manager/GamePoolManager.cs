using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePoolManager
{
    public static GamePoolManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GamePoolManager();
            }
            return sInstance;
        }
    }
    public void Init()
    {
        SkillPool = new Dictionary<SkillType, Queue<SkillBase>>();
        NpcPool = new Dictionary<string, Queue<NpcUnit>>();
    }

    public void Clear()
    {
        SkillPool.Clear();
        SkillPool = null;

        NpcPool.Clear();
        NpcPool = null;
    }

    public void EnqueueSkillPool(SkillBase InSkill)
    {
        if (SkillPool == null)
        {
            return;
        }
        if (SkillPool.ContainsKey(InSkill.mSkillType) == false)
        {
            SkillPool.Add(InSkill.mSkillType, new Queue<SkillBase>());
        }
        SkillPool[InSkill.mSkillType].Enqueue(InSkill);
    }

    public SkillBase DequeueSkillPool(SkillType InSkillType)
    {
        if (SkillPool == null)
        {
            return null;
        }
        if (SkillPool.ContainsKey(InSkillType) == false)
        {
            return null;
        }
        if (SkillPool[InSkillType].Count == 0)
        {
            return null;
        }
        return SkillPool[InSkillType].Dequeue();
    }

    public void EnqueueNpcPool(NpcUnit InNpcUnit)
    {
        string IUnitId = InNpcUnit.mStageUnitData.UnitId;
        if (NpcPool == null)
        {
            return;
        }
        if (NpcPool.ContainsKey(IUnitId) == false)
        {
            NpcPool.Add(IUnitId, new Queue<NpcUnit>());
        }
        NpcPool[IUnitId].Enqueue(InNpcUnit);
    }

    public NpcUnit DequeueNpcPool(string InUnitId)
    {
        if (NpcPool == null)
        {
            return null;
        }
        if (NpcPool.ContainsKey(InUnitId) == false)
        {
            return null;
        }
        if (NpcPool[InUnitId].Count == 0)
        {
            return null;
        }
        return NpcPool[InUnitId].Dequeue();
    }

    private static GamePoolManager sInstance = null;

    private Dictionary<SkillType, Queue<SkillBase>> SkillPool = null;
    private Dictionary<string, Queue<NpcUnit>> NpcPool = null;
}
