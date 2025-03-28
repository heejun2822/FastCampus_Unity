using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static UIManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new UIManager();
            }
            return sInstance;
        }
    }

    public delegate void OnShowHUDText(bool InIsShow, string Text);
    public OnShowHUDText aOnShowHUDText { get; set; }

    public delegate void OnSetExp(int InExp, int InMaxExp);
    public OnSetExp aOnSetExp { get; set; }

    public void ShowHUDText(string InText)
    {
        if (aOnShowHUDText != null)
        {
            aOnShowHUDText(true, InText);
        }
    }
    public void HideHUDText()
    {
        if (aOnShowHUDText != null)
        {
            aOnShowHUDText(false, string.Empty);
        }
    }

    public void SetExp(int InCurrentExp, int InMaxExp)
    {
        if (aOnSetExp != null)
        {
            aOnSetExp(InCurrentExp, InMaxExp);
        }
    }


    private static UIManager sInstance = null;
}
