using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDComponent : MonoBehaviour
{
    public GameObject RootLevelUpUI;
    public Text HUDText;
    public Slider MyUnitExpSlider;
    public Text GameTimeText;

    void Awake()
    {
        UIManager.aInstance.aOnShowHUDText += HandleShowHUDText;
        UIManager.aInstance.aOnSetExp += HandleSetExp;
        UIManager.aInstance.aOnShowLevelUpStateUI += HandleLevelUpStateUI;
        InitHUD();
    }
    void OnDestroy()
    {
        UIManager.aInstance.aOnShowHUDText -= HandleShowHUDText;
        UIManager.aInstance.aOnSetExp -= HandleSetExp;
        UIManager.aInstance.aOnShowLevelUpStateUI -= HandleLevelUpStateUI;
    }

    void Update()
    {
        float IGameTimeTotalSeconds = GameDataManager.aInstance.GetGameTime();
        TimeSpan INowTimespan = TimeSpan.FromSeconds(IGameTimeTotalSeconds);
        if (GameTimeText)
        {
            GameTimeText.text = string.Format("{0:D2}:{1:D2}", INowTimespan.Minutes, INowTimespan.Seconds);
        }
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

    private void HandleLevelUpStateUI(bool InIsShow)
    {
        if (RootLevelUpUI != null)
        {
            RootLevelUpUI.SetActive(InIsShow);
        }
    }
}
