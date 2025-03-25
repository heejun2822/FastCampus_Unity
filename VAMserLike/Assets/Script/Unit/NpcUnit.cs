using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcUnit : UnitBase
{
    public StageUnitData mStageUnitData { get; set; }
    public bool mIsMoveToTarget { get; set; } = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int InUnitId, StageUnitData InStageUnitData)
    {
        InitUnit(InUnitId, InStageUnitData.Hp, InStageUnitData.Power, InStageUnitData.Armor);
        mStageUnitData = InStageUnitData;
        mIsMoveToTarget = true;
        mIsNoneDamage = false;
        GameDataManager.aInstance.mLiveNpcUnitCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        MyPcUnit IMyPcUnit = other.GetComponent<MyPcUnit>();
        if (IMyPcUnit != null)
        {
            mIsMoveToTarget = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MyPcUnit IMyPcUnit = other.GetComponent<MyPcUnit>();
        if (IMyPcUnit != null)
        {
            mIsMoveToTarget = true;
        }
    }

    public void SetSpeed(float InSpeed)
    {
        mStageUnitData.UnitSpeed = InSpeed;
        NpcUnitMovement Movement = GetComponent<NpcUnitMovement>();
        if (Movement != null)
        {
            Movement.mSpeed = InSpeed;
        }
    }

    public override void OnHit(int InDamage)
    {
        if (FSMStageController.aInstance.IsPlayGame() == false)
        {
            return;
        }
        if (mIsNoneDamage == true)
        {
            return;
        }
        mIsNoneDamage = true;
        base.OnHit(InDamage);

        Debug.Log("Npc : " + gameObject.name + "Hp : " + mUnitData.Hp);
        if (mIsAlive)
        {
            StartCoroutine(_OnHitting());
        }
    }
    private IEnumerator _OnHitting()
    {
        yield return new WaitForSeconds(1.0f);
        mIsNoneDamage = false;
    }

    public override void OnDie()
    {
        base.OnDie();
        mIsAlive = false;
        gameObject.SetActive(false);
        GamePoolManager.aInstance.EnqueueNpcPool(this);
        GameDataManager.aInstance.mLiveNpcUnitCount = Mathf.Max(0, --GameDataManager.aInstance.mLiveNpcUnitCount);

        StopAllCoroutines();
    }

    private bool mIsNoneDamage = false;
}
