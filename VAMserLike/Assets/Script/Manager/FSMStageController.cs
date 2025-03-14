using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStageController
{
    public static FSMStageController aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new FSMStageController();
            }
            return sInstance;
        }
    }

    public void Init()
    {

    }

    public void Clear()
    {
        
    }

    private static FSMStageController sInstance = null;
}
