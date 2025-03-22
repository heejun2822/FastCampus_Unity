using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager
{
    public static SpawnManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new SpawnManager();
            }
            return sInstance;
        }
    }
    public void Init()
    {
        UnitId = 0;
        Units = new Dictionary<string, NpcUnit>();
        UnitKeyByIndex = new Dictionary<int, string>();

    }
    public void Clear()
    {
        UnitId = 0;
        ClearUnitData();
    }

    public void SetupSpawnPoint(GameObject InSpawnPoint)
    {
        mSpawnPointObject = InSpawnPoint;
    }

    public void AddUnitData(string InUnitStringId, StageUnitData InData)
    {
        if (Units.ContainsKey(InUnitStringId))
        {
            return;
        }
        NpcUnit NpcUnitObject = Resources.Load<NpcUnit>(InData.UnitPath);
        NpcUnitObject.mStageUnitData = InData;
        NpcUnitObject.SetSpeed(InData.UnitSpeed);

        if (NpcUnitObject != null)
        {
            Units.Add(InUnitStringId, NpcUnitObject);
            UnitKeyByIndex.Add(Units.Count, InUnitStringId);
        }
    }

    public void RemoveUnitData(string InUnitStringId)
    {
        if (Units.ContainsKey(InUnitStringId) == false)
        {
            return;
        }
        Units.Remove(InUnitStringId);
    }

    public void ClearUnitData()
    {
        Units.Clear();
        Units = null;

        UnitKeyByIndex.Clear();
        UnitKeyByIndex = null;
    }

    public NpcUnit GetRandomUnitData()
    {
        if (Units == null)
        {
            return null;
        }
        int IRandomPickIndex = Random.Range(1, UnitKeyByIndex.Count + 1);
        string IUnitStringId = UnitKeyByIndex[IRandomPickIndex];
        if (Units.ContainsKey(IUnitStringId) == false)
        {
            return null;
        }
        return Units[IUnitStringId];
    }

    public NpcUnit SpawnNpc(string InUnitStringId, Transform InParent, Vector3 InPosition)
    {
        if (Units.ContainsKey(InUnitStringId) == false)
        {
            return null;
        }

        NpcUnit ISpawnUnit = GamePoolManager.aInstance.DequeueNpcPool(InUnitStringId);
        if (ISpawnUnit == null)
        {
            ISpawnUnit = GameObject.Instantiate<NpcUnit>(Units[InUnitStringId], InParent);
            ISpawnUnit.gameObject.name = InUnitStringId + NpcIndex;
            NpcIndex++;
        }

        ISpawnUnit.Init(GenerateUnitId(), Units[InUnitStringId].mStageUnitData);
        ISpawnUnit.gameObject.SetActive(true);
        ISpawnUnit.transform.position = InPosition;

        return ISpawnUnit;
    }

    private int GenerateUnitId()
    {
        UnitId++;
        if (UnitId > 999999)
        {
            UnitId = 1;
        }
        return UnitId;
    }

    private GameObject mSpawnPointObject;
    private Dictionary<string, NpcUnit> Units;
    private Dictionary<int, string> UnitKeyByIndex;
    private int NpcIndex = 0; 
    private int UnitId = 0;

    private static SpawnManager sInstance = null;
}
