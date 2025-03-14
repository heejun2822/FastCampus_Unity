using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int mStageId = 1;
    public GameObject mMyPc;
    public Transform mNpcSpawnParent;
    public Transform mSkillObjectParent;
    public Transform mItemObjectParent;

    // Start is called before the first frame update
    void Start()
    {
        GamePoolManager.aInstance.Init();
        GameControl.aInstance.Init();
        SpawnManager.aInstance.Init();
        FSMStageController.aInstance.Init();
    }

    void OnDestroy()
    {
        GamePoolManager.aInstance.Clear();
        GameControl.aInstance.Clear();
        SpawnManager.aInstance.Clear();
        FSMStageController.aInstance.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
