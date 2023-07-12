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
        SoundManager.Instance.ChangeBgValue(musicSL.value);
    }

    public void changeEffect()
    {
        SoundManager.Instance.ChangeEtValue(effectSL.value);
    }

    public void setMusic()
    {
        SoundManager.Instance.ChangeBgOpen(musicToggle.isOn);
    }

    public void setEffect()
    {
        SoundManager.Instance.ChangeEtOpen(effectToggle.isOn);
    }

    public void exitPanel()
    {
        hideMe();
        BeginPanel.Instance.showMe();
    }



}
