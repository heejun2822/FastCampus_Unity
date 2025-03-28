using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NextStateButton : MonoBehaviour
{
    void Awake()
    {
        mCurrentButton = GetComponent<Button>();
        if (mCurrentButton != null)
        {
            mCurrentButton.onClick.AddListener(OnNextStateButtonClick);
        }
    }
    private void OnDestroy()
    {
        if (mCurrentButton != null)
        {
            mCurrentButton.onClick.RemoveAllListeners();
        }
    }
    public void OnNextStateButtonClick()
    {
        FSMStageController.aInstance.ChangeState(new FSMStageStateProgress());
        SkillManager MyPcSkillManager = GameDataManager.aInstance.GetMyPcObject().GetComponent<SkillManager>();
        if (MyPcSkillManager != null)
        {
            MyPcSkillManager.AddSkillData(SkillType.Missile);
        }
    }
    private Button mCurrentButton;
}
