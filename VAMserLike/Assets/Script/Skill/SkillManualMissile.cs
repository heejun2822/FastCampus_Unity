using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManualMissile : SkillBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FireSkill(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InStartDir)
    {
        base.FireSkill(InSkillData, InStartPos, InStartDir);
        mSkillType = SkillType.ManualMissile;
        StartCoroutine(_OnMissileLiftTime());
    }

    public IEnumerator _OnMissileLiftTime()
    {
        float CurrentLifeTime = 0.0f;
        while (true)
        {
            Vector3 AddForceVector = mStartDir * mActiveSkillData.ActiveSkillLevelData.Speed * Time.deltaTime;
            transform.position += new Vector3(AddForceVector.x, 0, AddForceVector.z);
            CurrentLifeTime += Time.deltaTime;
            if (CurrentLifeTime > 2.0f)
            {
                break;
            }
            yield return null;
        }
        StopSkill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
        {
            return;
        }
        NpcUnit TargetNpcUnit = other.GetComponent<NpcUnit>();
        if (TargetNpcUnit != null)
        {
            TargetNpcUnit.OnHit(mActiveSkillData.ActiveSkillLevelData.Power);
            StopSkill();
        }
    }
}
