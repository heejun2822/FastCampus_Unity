using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateBase
{
    public EFSMStageStateType mCurrentStateType { get; protected set; }
    public FSMStateBase(EFSMStageStateType InType)
    {
        mCurrentStateType = InType;
    }

    public virtual void OnEnter()
    {

    }
    public virtual void OnExit()
    {

    }
    public virtual void OnProgress(float InDeltaTime)
    {
        
    }
}
