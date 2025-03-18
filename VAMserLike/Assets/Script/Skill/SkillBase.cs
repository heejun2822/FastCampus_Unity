using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public SkillType mSkillType { set; get; }
    public Vector3 mStartPos { get; private set; }
    public Vector3 mStartDir { get; private set; }
    public ActiveSkillData mActiveSkillData { get; private set; }
    public virtual void FireSkill(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InStartDir)
    {
        mActiveSkillData = InSkillData;
        mStartPos = InStartPos;
        mStartDir = InStartDir;

        transform.position = mStartPos;
    }

    public virtual void StopSkill()
    {
        gameObject.SetActive(false);
        GamePoolManager.aInstance.EnqueueSkillPool(this);
    }

    public virtual void Update()
    {

    }
    public virtual void OnDestroy()
    {
        
    }
}
