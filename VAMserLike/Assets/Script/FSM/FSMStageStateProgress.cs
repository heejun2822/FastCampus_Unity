using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FSMStageStateProgress : FSMStateBase
{
    public FSMStageStateProgress()
        : base(EFSMStageStateType.StageProgress)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Stage State Progress Enter");

        mNowSpawn = 10;
        mDurationTime = 0.0f;
        mNextSpawnTime = 0.0f;

        mMyPcObj = GameDataManager.aInstance.GetMyPcObject();
        mSpawnRoot = GameDataManager.aInstance.GetSpawnRootTransform();

        int CurrentStage = GameDataManager.aInstance.mStage;
        StageData CurrentStageData = GameDataManager.aInstance.FindStageData(CurrentStage);
        if (CurrentStageData != null)
        {
            mMaxSpawn = CurrentStageData.MaxSpawnCount;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnProgress(float InDeltaTime)
    {
        base.OnProgress(InDeltaTime);

        mDurationTime += InDeltaTime;
        bool bSpawn = false;
        if (mDurationTime > mNextSpawnTime)
        {
            mNowSpawn = 1;
            mNowSpawn = Mathf.Min(mNowSpawn, mMaxSpawn);
            mNextSpawnTime += 3.0f;
            bSpawn = true;
        }
        if (bSpawn)
        {
            if (GameDataManager.aInstance.mLiveNpcUnitCount > 100)
            {
                return;
            }
            Vector3 IPivotPos = mMyPcObj.transform.position;

            for (int i = 0; i < mNowSpawn; i++)
            {
                NpcUnit NewSpawnUnit = SpawnManager.aInstance.GetRandomUnitData();
                Vector2 IRandomCircle = Random.insideUnitCircle.normalized;
                float IRandomFactor = Random.Range(10.0f, 12.0f);
                Vector3 ISpawnPosition = IPivotPos + new Vector3(IRandomCircle.x * IRandomFactor, 5, IRandomCircle.y * IRandomFactor);
                SpawnManager.aInstance.SpawnNpc(NewSpawnUnit.mStageUnitData.UnitId, mSpawnRoot, ISpawnPosition);
            }
        }
    }

    private GameObject mMyPcObj;
    private Transform mSpawnRoot;

    private float mDurationTime = 0.0f;
    private int mNowSpawn = 0;
    private int mMaxSpawn = 0;
    private float mNextSpawnTime = 0.0f;
}
