using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FSMStageStateEnter : FSMStateBase
{
    public FSMStageStateEnter()
        : base(EFSMStageStateType.StageStart)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        mCountDown = 0;
        mDurationTime = 0;

        int ICurrentStageId = GameDataManager.aInstance.mStage;
        StageData ICurrentStageData = GameDataManager.aInstance.FindStageData(ICurrentStageId);
        if (ICurrentStageData != null)
        {
            foreach (StageUnitData EachNpc in ICurrentStageData.Units)
            {
                SpawnManager.aInstance.AddUnitData(EachNpc.UnitId, EachNpc);
            }
        }
        MyPcUnit MyPc = GameDataManager.aInstance.GetMyPcObject().GetComponent<MyPcUnit>();
        if (MyPc != null)
        {
            MyPc.InitUnit(0, 10000, 100, 100);
            SkillManager MySkillManager = MyPc.GetComponent<SkillManager>();
            MySkillManager.AddSkillData(SkillType.Missile);
            MySkillManager.AddSkillData(SkillType.ManualMissile);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Stage State Enter, Call Exit");
        mDurationTime = 0;
    }

    public override void OnProgress(float InDeltaTime)
    {
        base.OnProgress(InDeltaTime);
        mDurationTime += InDeltaTime;
        if (mDurationTime > 1.0f)
        {
            if (mCountDown <= 0)
            {
                FSMStageController.aInstance.ChangeState(new FSMStageStateProgress());
            }
            else
            {
                mCountDown--;
                Debug.Log("Count Down - " + mCountDown);
            }
            mDurationTime = 0.0f;
        }
    }

    private int mCountDown = 3;
    private float mDurationTime = 0;
}
