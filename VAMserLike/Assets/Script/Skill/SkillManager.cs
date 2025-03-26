using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    void Awake()
    {
        LevelOfSkills = new Dictionary<SkillType, int>();
        CurrentActiveSkillDatas = new Dictionary<SkillType, ActiveSkillData>();
        CurrentManualSkillDatas = new List<ActiveSkillData>();

        GameControl.aInstance.aOnMouseInput += _OnMouseInput;
    }

    void OnDestroy()
    {
        LevelOfSkills.Clear();
        LevelOfSkills = null;

        CurrentActiveSkillDatas.Clear();
        CurrentActiveSkillDatas = null;

        CurrentManualSkillDatas.Clear();
        CurrentManualSkillDatas = null;

        GameControl.aInstance.aOnMouseInput -= _OnMouseInput;
    }
    // Update is called once per frame
    void Update()
    {
        if (FSMStageController.aInstance.IsPlayGame() == false)
        {
            return;
        }
        foreach (var EachActiveSkill in CurrentActiveSkillDatas)
        {
            EachActiveSkill.Value.CurrentCoolTime += Time.deltaTime;
            if (EachActiveSkill.Value.CurrentCoolTime >= EachActiveSkill.Value.ActiveSkillLevelData.CoolTime)
            {
                if (EachActiveSkill.Value.ActiveType == SkillActiveType.Auto)
                {
                    FireSkill(EachActiveSkill.Value);
                }
            }
        }
        CurrentCooltime += Time.deltaTime;
    }

    private void _OnMouseInput(int InIndex, Vector3 InMousePos)
    {
        if (FSMStageController.aInstance.IsPlayGame() == false)
        {
            return;
        }

        if (CurrentManualSkillDatas.Count - 1 < InIndex)
        {
            return;
        }
        if (CurrentManualSkillDatas[InIndex].CurrentCoolTime < CurrentManualSkillDatas[InIndex].ActiveSkillLevelData.CoolTime)
        {
            return;
        }

        RaycastHit IHit;
        Ray IRay = Camera.main.ScreenPointToRay(InMousePos);
        int layermask = 1 << LayerMask.NameToLayer("Terrain");
        if (Physics.Raycast(IRay, out IHit, 1000, layermask))
        {
            CurrentManualSkillDatas[InIndex].FirePosition = IHit.point;
            FireSkill(CurrentManualSkillDatas[InIndex]);
        }
    }

    public void AddSkillData(SkillType InSkillType)
    {
        SkillData ISkillData = GameDataManager.aInstance.FindSkillData(InSkillType);
        if (ISkillData == null)
        {
            return;
        }
        if (LevelOfSkills.ContainsKey(InSkillType))
        {
            LevelOfSkills[InSkillType]++;
        }
        else
        {
            LevelOfSkills.Add(InSkillType, 1);
        }

        int ICurrentSkillLevel = LevelOfSkills[InSkillType];
        SkillLevelData ICurrentSkillLevelData = GameDataManager.aInstance.FindSkillLevelData(InSkillType, ICurrentSkillLevel);
        if (ICurrentSkillLevelData == null)
        {
            return;
        }

        if (CurrentActiveSkillDatas.ContainsKey(InSkillType) == false)
        {
            ActiveSkillData NewSkillData = new ActiveSkillData();
            NewSkillData.Type = InSkillType;
            NewSkillData.ActiveType = ISkillData.ActiveType;
            NewSkillData.CurrentCoolTime = 0.0f;
            NewSkillData.ActiveSkillLevelData = ICurrentSkillLevelData;

            CurrentActiveSkillDatas.Add(InSkillType, NewSkillData);
        }
        else
        {
            CurrentActiveSkillDatas[InSkillType].CurrentCoolTime = 0.0f;
            CurrentActiveSkillDatas[InSkillType].ActiveSkillLevelData = ICurrentSkillLevelData;
        }

        switch (ISkillData.ActiveType)
        {
            case SkillActiveType.Manual:
                {
                    int IFindIndex = -1;
                    int ICurrentIndex = 0;
                    foreach (var EachSkill in CurrentManualSkillDatas)
                    {
                        if (EachSkill.Type == InSkillType)
                        {
                            IFindIndex = ICurrentIndex;
                        }
                        ICurrentIndex++;
                    }

                    if (IFindIndex >= 0)
                    {
                        CurrentManualSkillDatas[IFindIndex] = CurrentActiveSkillDatas[InSkillType];
                    }
                    else
                    {
                        CurrentManualSkillDatas.Add(CurrentActiveSkillDatas[InSkillType]);
                    }
                }
                break;
        }
    }

    public ActiveSkillData GetCurrentSkillData(SkillType InSkillType)
    {
        if (CurrentActiveSkillDatas.ContainsKey(InSkillType) == false)
        {
            return null;
        }
        return CurrentActiveSkillDatas[InSkillType];
    }

    public void FireSkill(ActiveSkillData InSkillData)
    {
        switch (InSkillData.Type)
        {
            case SkillType.Missile:
                {
                    for (int fireAngle = 0; fireAngle < 360; fireAngle += 10)
                    {
                        Vector3 ShotDirection = new Vector3(Mathf.Cos(fireAngle * Mathf.Deg2Rad),
                                                            1,
                                                            Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                        Vector3 StartPos = new Vector3(transform.position.x, 1, transform.position.z);

                        FireSkillObject(InSkillData, StartPos, ShotDirection);
                    }
                }
                break;
            case SkillType.ManualMissile:
                {
                    Vector3 ShotDirection = (InSkillData.FirePosition - transform.position).normalized;
                    Vector3 startPos = new Vector3(transform.position.x, 1, transform.position.z);
                    FireSkillObject(InSkillData, startPos, ShotDirection);
                }
                break;
        }
        InSkillData.CurrentCoolTime = 0;
    }

    public void FireSkillObject(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InSkillDir)
    {
        SkillBase SkillObject = GamePoolManager.aInstance.DequeueSkillPool(InSkillData.Type);
        if (SkillObject == null)
        {
            SkillBase NewSkillObjectPrefab = GameDataManager.aInstance.GetSkillObjectPrefab(InSkillData.Type, InSkillData.ActiveSkillLevelData.Level);
            SkillObject = GameObject.Instantiate(NewSkillObjectPrefab, GameDataManager.aInstance.GetSkillRootTransform());
            if (SkillObject == null)
            {
                return;
            }
        }
        SkillObject.gameObject.SetActive(true);
        SkillObject.mSkillType = InSkillData.Type;
        SkillObject.FireSkill(InSkillData, InStartPos, InSkillDir);
    }

    public float CurrentCooltime = 0.0f;

    // 스킬 타입별 레벨 정보
    Dictionary<SkillType, int> LevelOfSkills;

    // 현재 사용중인 스킬 정보
    Dictionary<SkillType, ActiveSkillData> CurrentActiveSkillDatas;
    List<ActiveSkillData> CurrentManualSkillDatas;
}
