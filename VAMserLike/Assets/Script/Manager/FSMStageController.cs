using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStageController
{
    public static FSMStageController aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new FSMStageController();
            }
            return sInstance;
        }
    }
    public void Init()
    {

    }
    public void Clear()
    {
        
    }

    public void EnterStage()
    {
        mStageFSM = new FSM(new FSMStageStateEnter());
    }

    public void ChangeState(FSMStateBase InFSMState)
    {
        if (mStageFSM != null)
        {
            mStageFSM.ChangeState(InFSMState);
        }
        Debug.Log("Change State : " + InFSMState.mCurrentStateType.ToString());
    }

    public void OnUpdate(float InDeltaTime)
    {
        if (mStageFSM != null)
        {
            mStageFSM.OnUpdateState(InDeltaTime);
        }
    }

    public bool IsPlayGame()
    {
        if (mStageFSM == null)
        {
            return false;
        }
        return mStageFSM.mCurrentState.mCurrentStateType == EFSMStageStateType.StageProgress
                || mStageFSM.mCurrentState.mCurrentStateType == EFSMStageStateType.StageBoss;
    }
    private FSM mStageFSM = null;
    private static FSMStageController sInstance = null;
}
