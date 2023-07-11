using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSlider : BasePanel<SettingSlider>
{

    void Start()
    {
        hideMe();
    }
    public void changeMusic(float n)
    {

    }

    public void changeEffect(float n)
    {

    }

    public void setMusic(bool b)
    {

    }

    public void setEffect(bool b)
    {

    }

    public void exitPanel()
    {
        hideMe();
        BeginPanel.Instance.showMe();
    }



}
