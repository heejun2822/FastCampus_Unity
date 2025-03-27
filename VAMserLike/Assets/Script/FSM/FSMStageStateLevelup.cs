using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameControl;

public class FSMStageStateLevelup : FSMStateBase
{
    public FSMStageStateLevelup()
        : base(EFSMStageStateType.StageLevelup)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        GameControl.aInstance.aOnMouseInput += _OnMouseInput;
    }

    public override void OnExit()
    {
        base.OnExit();
        GameControl.aInstance.aOnMouseInput -= _OnMouseInput;
    }

    public override void OnProgress(float InDeltaTime)
    {
        base.OnProgress(InDeltaTime);
    }

    private void _OnMouseInput(int InIndex, Vector3 InMousePos)
    {
        FSMStageController.aInstance.ChangeState(new FSMStageStateProgress());
        SkillManager MyPcSkillManager = GameDataManager.aInstance.GetMyPcObject().GetComponent<SkillManager>();
        if (MyPcSkillManager != null)
        {
            MyPcSkillManager.AddSkillData(SkillType.Missile);
        }
    }
}
