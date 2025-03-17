using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    void Awake()
    {
        GameControl.aInstance.aOnMouseInput += _OnMouseInput;
    }

    void OnDestroy()
    {
        GameControl.aInstance.aOnMouseInput -= _OnMouseInput;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void _OnMouseInput(int InIndex, Vector3 InMousePos)
    {
        RaycastHit IHit;
        Ray IRay = Camera.main.ScreenPointToRay(InMousePos);
        int layermask = 1 << LayerMask.NameToLayer("Terrain");

        if (Physics.Raycast(IRay, out IHit, 1000, layermask))
        {
            ActiveSkillData NewSkillData = new ActiveSkillData();
            NewSkillData.FirePosition = IHit.point;
            FireSkill(NewSkillData);
        }
    }

    public void FireSkill(ActiveSkillData InSkillData)
    {
        Vector3 ShotDirection = (InSkillData.FirePosition - transform.position).normalized;
        Vector3 startPos = new Vector3(transform.position.x, 1, transform.position.z);

        FireSkillObject(InSkillData, startPos, ShotDirection);
    }

    public void FireSkillObject(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InSkillDir)
    {
        SkillBase SkillObject = GamePoolManager.aInstance.DequeueSkillPool(InSkillData.Type);
        if (SkillObject == null)
        {
            SkillBase NewSkillObjectPrefab = Resources.Load<SkillBase>("Prefabs/Missile");
            SkillObject = GameObject.Instantiate(NewSkillObjectPrefab, GameDataManager.aInstance.GetSkillRootTransform());
            if (SkillObject == null)
            {
                return;
            }
            SkillObject.transform.position = InStartPos;
        }
    }
}
