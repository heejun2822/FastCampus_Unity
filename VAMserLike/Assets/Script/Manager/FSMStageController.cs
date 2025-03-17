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
    }

    public void OnUpdate(float InDeltaTime)
    {
        if (mStageFSM != null)
        {
            mStageFSM.OnUpdateState(InDeltaTime);
        }
    }
    private FSM mStageFSM = null;
    private static FSMStageController sInstance = null;
}
