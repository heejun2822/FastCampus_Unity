using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePoolManager
{
    public static GamePoolManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GamePoolManager();
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

    private static GamePoolManager sInstance = null;
}
