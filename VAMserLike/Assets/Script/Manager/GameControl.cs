using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl
{
    public static GameControl aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameControl();
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

    private static GameControl sInstance = null;
}
