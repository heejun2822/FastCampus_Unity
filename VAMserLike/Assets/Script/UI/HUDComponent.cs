using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDComponent : MonoBehaviour
{
    public Text HUDText;
    public Slider MyUnitExpSlider;

    void Awake()
    {
        UIManager.aInstance.aOnShowHUDText += HandleShowHUDText;
        UIManager.aInstance.aOnSetExp += HandleSetExp;
        InitHUD();
    }

    void OnDestroy()
    {
        UIManager.aInstance.aOnShowHUDText -= HandleShowHUDText;
        UIManager.aInstance.aOnSetExp -= HandleSetExp;
    }

    protected void InitHUD()
    {
        if (HUDText != null)
        {
            HUDText.text = string.Empty;
            HUDText.gameObject.SetActive(false);
        }
    }

    public void HandleShowHUDText(bool InIsShow, string InText)
    {
        if (HUDText == null)
        {
            return;
        }

        HUDText.gameObject.SetActive(InIsShow);
        HUDText.text = InText;
    }

    private void HandleSetExp(int InCurrentExp, int InMaxExp)
    {
        float IExpValue = (float)InCurrentExp / InMaxExp;
        if (MyUnitExpSlider != null)
        {
            MyUnitExpSlider.value = IExpValue;
        }
    }

    void Update()
    {
        
    }
}
