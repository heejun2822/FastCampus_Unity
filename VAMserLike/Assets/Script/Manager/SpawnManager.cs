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

    }

    public void Clear()
    {
        
    }

    private static SpawnManager sInstance = null;
}
