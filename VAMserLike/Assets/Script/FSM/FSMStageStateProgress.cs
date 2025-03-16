using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FSMStageStateProgress : FSMStateBase
{
    public FSMStageStateProgress()
        : base(EFSMStageStateType.StageStart)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Stage State Progress Enter");
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnProgress(float InDeltaTime)
    {
        base.OnProgress(InDeltaTime);
    }
}
