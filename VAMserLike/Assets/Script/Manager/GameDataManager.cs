using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    public int mStage { get; private set; }
    public static GameDataManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameDataManager();
            }
            return sInstance;
        }
    }
    public GameObject GetMyPcObject()
    {
        return mMyPc;
    }
    public Transform GetSpawnRootTransform()
    {
        return mSpawnRoot;
    }
    public Transform GetItemRootTransform()
    {
        return mItemRoot;
    }
    public Transform GetSkillRootTransform()
    {
        return mSkillRoot;
    }

    public void Init()
    {

    }
    public void Clear()
    {
        mStage = 0;
        mMyPc = null;
        mSpawnRoot = null;
        mItemRoot = null;
        mSkillRoot = null;
    }
    public void SetStageData(GameObject InMyPc, Transform InSpawnRoot, Transform InSkillRoot, Transform InItemRoot)
    {
        mMyPc = InMyPc;
        mSpawnRoot = InSpawnRoot;
        mSkillRoot = InSkillRoot;
        mItemRoot = InItemRoot;
    }
    public void SetCurrentStage(int InStage)
    {
        mStage = InStage;
    }

    private static GameDataManager sInstance = null;

    private GameObject mMyPc;
    private Transform mSpawnRoot;
    private Transform mSkillRoot;
    private Transform mItemRoot;
}
