using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameDataManager
{
    public int mStage { get; private set; }
    public int mLiveNpcUnitCount { get; set; } = 0;
    public static GameDataManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameDataManager();
            }
            return sInstance;
        }
    }
    public GameObject GetMyPcObject()
    {
        return mMyPc;
    }
    public Transform GetSpawnRootTransform()
    {
        return mSpawnRoot;
    }
    public Transform GetItemRootTransform()
    {
        return mItemRoot;
    }
    public Transform GetSkillRootTransform()
    {
        return mSkillRoot;
    }

    public void Init()
    {

    }
    public void Clear()
    {
        mStage = 0;
        mMyPc = null;
        mSpawnRoot = null;
        mItemRoot = null;
        mSkillRoot = null;

        StageDatas.Clear();
        StageDatas = null;

        SkillDatas.Clear();
        SkillDatas = null;

        SkillResources.Clear();
        SkillResources = null;

        ItemDatas.Clear();
        ItemDatas = null;

        ItemResources.Clear();
        ItemResources = null;

        mLiveNpcUnitCount = 0;
    }

    public void LoadAll()
    {
        LoadStageData();
        LoadSkillData();
        LoadItemData();
    }
    protected void LoadItemData()
    {
        ItemDatas = new Dictionary<string, ItemData>();
        ItemResources = new Dictionary<string, ItemBase>();
        TextAsset JsonTextAsset = Resources.Load<TextAsset>("Data/Items");
        string IJson = JsonTextAsset.text;
        JObject IDataObject = JObject.Parse(IJson);
        JToken IToken = IDataObject["Items"];
        JArray IArray = IToken.Value<JArray>();
        foreach (JObject EachObject in IArray)
        {
            ItemData NewItemData = new ItemData();
            NewItemData.Id = EachObject.Value<string>("Id");
            NewItemData.Path = EachObject.Value<string>("Path");
            string ItemTypeString = EachObject.Value<string>("Type");
            NewItemData.Type = Enum.Parse<EItemType>(ItemTypeString);
            NewItemData.Value = EachObject.Value<int>("Value");

            ItemDatas.Add(NewItemData.Id, NewItemData);
            Debug.Log("New Item Add Complete : " + NewItemData.ShowItemDataLog());

            ItemBase ItemObject = Resources.Load<ItemBase>(NewItemData.Path);
            if (ItemObject == null)
            {
                Debug.LogError("Not Exist Path Prefabs : " + NewItemData.Path);
            }
            else
            {
                ItemResources.Add(NewItemData.Id, ItemObject);
            }
        }
    }

    protected void LoadStageData()
    {
        StageDatas = new Dictionary<int, StageData>();
        StageDatas.Clear();

        TextAsset StageJsonTextAsset = Resources.Load<TextAsset>("Data/StageDatas");
        string IStageJson = StageJsonTextAsset.text;
        JObject IStageDataObject = JObject.Parse(IStageJson);
        JToken IStageToken = IStageDataObject["Stages"];
        JArray IStageArray = IStageToken.Value<JArray>();

        foreach (JObject EachObject in IStageArray)
        {
            StageData NewStageData = new StageData();
            NewStageData.StageId = EachObject.Value<int>("StageId");
            NewStageData.MaxSpawnCount = EachObject.Value<int>("MaxSpawn");
            NewStageData.DropId = EachObject.Value<string>("DropId");
            JArray INpcArray = EachObject.Value<JArray>("UnitPaths");
            foreach (JObject EachNpc in INpcArray)
            {
                StageUnitData UnitData = new StageUnitData();
                UnitData.UnitId = EachNpc.Value<string>("Id");
                UnitData.UnitPath = EachNpc.Value<string>("Path");
                UnitData.UnitSpeed = EachNpc.Value<float>("Speed");
                UnitData.Hp = EachNpc.Value<int>("Hp");
                UnitData.Armor = EachNpc.Value<int>("Armor");
                UnitData.Power = EachNpc.Value<int>("Power");
                NewStageData.Units.Add(UnitData);
            }
            StageDatas.Add(NewStageData.StageId, NewStageData);
        }
    }

    public ItemData GetItemData(string InItemId)
    {
        if (ItemDatas.ContainsKey(InItemId) == false)
        {
            return null;
        }
        return ItemDatas[InItemId];
    }

    public ItemBase GetItemObject(string InItemId)
    {
        if (ItemResources.ContainsKey(InItemId) == false)
        {
            return null;
        }
        return ItemResources[InItemId];
    }

    public StageData FindStageData(int InStageId)
    {
        if (StageDatas.ContainsKey(InStageId) == false)
        {
            return null;
        }
        return StageDatas[InStageId];
    }

    protected void LoadSkillData()
    {
        SkillDatas = new Dictionary<SkillType, SkillData>();
        SkillResources = new Dictionary<string, SkillBase>();
        SkillDatas.Clear();
        SkillResources.Clear();

        TextAsset SkillJsonTextAsset = Resources.Load<TextAsset>("Data/SkillDatas");
        string ISkillJson = SkillJsonTextAsset.text;
        JObject IDataObject = JObject.Parse(ISkillJson);
        JToken IToken = IDataObject["Skills"];
        JArray IArray = IToken.Value<JArray>();

        foreach (JObject EachObject in IArray)
        {
            SkillData NewSkillData = new SkillData();
            NewSkillData.Type = Enum.Parse<SkillType>(EachObject.Value<string>("Type"));
            NewSkillData.ActiveType = Enum.Parse<SkillActiveType>(EachObject.Value<string>("ActiveType"));
            NewSkillData.LevelDatas = new Dictionary<int, SkillLevelData>();
            JArray ILevelArray = EachObject.Value<JArray>("LevelDatas");
            foreach (JObject EachLevel in ILevelArray)
            {
                SkillLevelData NewSkillLevelData = new SkillLevelData();
                NewSkillLevelData.Type = NewSkillData.Type;
                NewSkillLevelData.Level = EachLevel.Value<int>("Level");
                NewSkillLevelData.Path = EachLevel.Value<string>("Path");
                NewSkillLevelData.Power = EachLevel.Value<int>("Power");
                NewSkillLevelData.Size = EachLevel.Value<int>("Size");
                NewSkillLevelData.Speed = EachLevel.Value<float>("Speed");
                NewSkillLevelData.ActiveTime = EachLevel.Value<float>("ActiveTime");
                NewSkillLevelData.CoolTime = EachLevel.Value<float>("CoolTime");

                NewSkillData.LevelDatas.Add(NewSkillLevelData.Level, NewSkillLevelData);

                SkillBase SkillObject = Resources.Load<SkillBase>(NewSkillLevelData.Path);
                string SkillId = GetSkillId(NewSkillLevelData);
                SkillResources.Add(SkillId, SkillObject);
            }
            SkillDatas.Add(NewSkillData.Type, NewSkillData);
        }
    }

    public string GetSkillId(SkillLevelData InSkillLevelData)
    {
        return GetSkillId(InSkillLevelData.Type, InSkillLevelData.Level);
    }

    public string GetSkillId(SkillType InSkillType, int InLevel)
    {
        return string.Format("{0}_{1}", InSkillType.ToString(), InLevel);
    }

    public SkillData FindSkillData(SkillType InSkillType)
    {
        if (SkillDatas.ContainsKey(InSkillType) == false)
        {
            return null;
        }
        return SkillDatas[InSkillType];
    }

    public SkillLevelData FindSkillLevelData(SkillType InSkillType, int InSkillLevel)
    {
        if (SkillDatas.ContainsKey(InSkillType) == false)
        {
            return null;
        }
        if (SkillDatas[InSkillType].LevelDatas.ContainsKey(InSkillLevel) == false)
        {
            return null;
        }
        return SkillDatas[InSkillType].LevelDatas[InSkillLevel];
    }

    public SkillBase GetSkillObjectPrefab(SkillLevelData InSkillLevelData)
    {
        return GetSkillObjectPrefab(GetSkillId(InSkillLevelData));
    }

    public SkillBase GetSkillObjectPrefab(SkillType InType, int InSkillLevel)
    {
        return GetSkillObjectPrefab(GetSkillId(InType, InSkillLevel));
    }
    public SkillBase GetSkillObjectPrefab(string InSkillId)
    {
        if (SkillResources.ContainsKey(InSkillId) == false)
        {
            return null;
        }
        return SkillResources[InSkillId];
    }

    public void SetStageData(GameObject InMyPc, Transform InSpawnRoot, Transform InSkillRoot, Transform InItemRoot)
    {
        mMyPc = InMyPc;
        mSpawnRoot = InSpawnRoot;
        mSkillRoot = InSkillRoot;
        mItemRoot = InItemRoot;
    }
    public void SetCurrentStage(int InStage)
    {
        mStage = InStage;
    }

    private static GameDataManager sInstance = null;

    private GameObject mMyPc;
    private Transform mSpawnRoot;
    private Transform mSkillRoot;
    private Transform mItemRoot;

    private Dictionary<SkillType, SkillData> SkillDatas = null;
    private Dictionary<string, SkillBase> SkillResources = null;

    private Dictionary<int, StageData> StageDatas = null;

    private Dictionary<string, ItemData> ItemDatas = null;
    private Dictionary<string, ItemBase> ItemResources = null;
}
