using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public FSM(FSMStateBase InFSMState)
    {
        mCurrentState = InFSMState;
        if (mCurrentState != null)
        {
            mCurrentState.OnEnter();
        }
    }

    public void ChangeState(FSMStateBase InFSMState)
    {
        if (mCurrentState == InFSMState)
        {
            return;
        }

        if (mCurrentState != null)
        {
            mCurrentState.OnExit();
        }

        mCurrentState = InFSMState;
        if (mCurrentState != null)
        {
            mCurrentState.OnEnter();
        }
    }

    public void OnUpdateState(float InDeltaTime)
    {
        if (mCurrentState != null)
        {
            mCurrentState.OnProgress(InDeltaTime);
        }
    }

    public FSMStateBase mCurrentState { get; private set; }
}
