using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingSlider : BasePanel<SettingSlider>
{

    public Slider musicSL;
    public Toggle musicToggle;
    public Slider effectSL;
    public Toggle effectToggle;


    void Start()
    {
        hideMe();
    }
    public void changeMusic()
    {
        GameDataManager.Instance.changeBGValue(musicSL.value);
        SoundManager.Instance.ChangeBgValue(musicSL.value);
        
    }

    public void changeEffect()
    {
        GameDataManager.Instance.changeEffectValue(effectSL.value);
        SoundManager.Instance.ChangeEtValue(effectSL.value);
        
    }

    public void setMusic()
    {
        GameDataManager.Instance.oepnOrCloseBGMusic(musicToggle.isOn);
        SoundManager.Instance.ChangeBgOpen(musicToggle.isOn);
        
    }

    public void setEffect()
    {
        GameDataManager.Instance.oepnOrCloseEffect(effectToggle.isOn);
        SoundManager.Instance.ChangeEtOpen(effectToggle.isOn);
        
    }

    public void exitPanel()
    {
        hideMe();
        BeginPanel.Instance.showMe();
    }

    public void updatePanelInfor()
    {
        MusicData data = GameDataManager.Instance.musicData;
        musicSL.value = data.bgValue;
        effectSL.value = data.effectValue;
        musicToggle.isOn = data.isOpenBG;
        effectToggle.isOn = data.isOpenEffect;
    }

    public override void showMe()
    {
        base.showMe();
        //每次显示的时候更新面板
        updatePanelInfor();
    }



}
