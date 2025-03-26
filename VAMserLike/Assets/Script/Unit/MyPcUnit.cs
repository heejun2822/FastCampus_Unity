using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyPcUnit : UnitBase
{
    public int mMaxExp { get; set; }
    public int mExp { get; set; }
    public int mLevel { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        HittedNpcs = new Dictionary<int, NpcUnit>();
    }

    private void OnDestroy()
    {
        HittedNpcs.Clear();
        HittedNpcs = null;
    }
    public override void InitUnit(int InUnitId, int InHp, int InPower, int InArmor)
    {
        base.InitUnit(InUnitId, InHp, InPower, InArmor);
        mExp = 0;
        mMaxExp = MAX_EXP_FROM_LEVEL_VALUE;
        mLevel = 1;
    }

    public void SetupLevel(int InLevel)
    {
        mLevel = InLevel;
        mExp = 0;
        mMaxExp = MAX_EXP_FROM_LEVEL_VALUE * mLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        NpcUnit HittedNpc = other.GetComponent<NpcUnit>();
        if (HittedNpc != null)
        {
            if (HittedNpcs.ContainsKey(HittedNpc.mUnitId) == false)
            {
                HittedNpcs.Add(HittedNpc.mUnitId, HittedNpc);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NpcUnit HittedNpc = other.GetComponent<NpcUnit>();
        if (HittedNpc != null)
        {
            HittedNpcs.Remove(HittedNpc.mUnitId);
        }
    }

    public override void OnHit(int InDamage)
    {
        base.OnHit(InDamage);
        Debug.Log("Npc : " + gameObject.name + " / Hp : " + mUnitData.Hp);
    }
    public override void OnDie()
    {
        base.OnDie();
        FSMStageController.aInstance.ChangeState(new FSMStageStateExit());
    }
    public override void OnGetterItem(ItemBase InItemBase)
    {
        base.OnGetterItem(InItemBase);
        // TODO :: 아이템 타입별 어떤 처리를 할지를 이곳에서 구현
    }
    // Update is called once per frame
    void Update()
    {
        List<int> IRemoveUnitIds = new List<int>();
        foreach (var EachUnit in HittedNpcs)
        {
            if (EachUnit.Value.mIsAlive == false)
            {
                IRemoveUnitIds.Add(EachUnit.Value.mUnitId);
            }
            else
            {
                OnHit(EachUnit.Value.mStageUnitData.Power);
            }
        }
        foreach (int EachRemoveUnitId in IRemoveUnitIds)
        {
            HittedNpcs.Remove(EachRemoveUnitId);
        }
    }

    Dictionary<int, NpcUnit> HittedNpcs;
    private const int MAX_EXP_FROM_LEVEL_VALUE = 10000;
}
