using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingSlider : BasePanel<SettingSlider>
{
    public Slider musicSL;
    public Toggle musicToggle;

    void Start()
    {
        hideMe();
    }
    public void changeMusic()
    {
        SoundManager.Instance.ChangeValue(musicSL.value);
    }

    public void changeEffect()
    {

    }

    public void setMusic()
    {
        SoundManager.Instance.ChangeOpen(musicToggle.isOn);
    }

    public void setEffect()
    {

    }

    public void exitPanel()
    {
        hideMe();
        BeginPanel.Instance.showMe();
    }



}
