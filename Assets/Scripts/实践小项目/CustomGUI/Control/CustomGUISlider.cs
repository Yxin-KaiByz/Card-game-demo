using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_Slider_Type
{
    Horizontal,
    Vertical,
}

public class CustomGUISlider : CustomGUIControl
{
    //最小值
    public float minValue = 0;
    //最大值
    public float maxValue = 1;
    //当前值
    public float nowValue = 0;
    //水平还是竖直样式
    public E_Slider_Type type = E_Slider_Type.Horizontal;
    //小按钮的 style
    public GUIStyle styleThumb;

    public event UnityAction<float> changeValue;

    private float oldValue = 0;

    protected override void StyleOffDraw()
    {
        switch (type)
        {
            case E_Slider_Type.Horizontal:
                nowValue = GUI.HorizontalSlider(guiPos.Pos, nowValue, minValue, maxValue);
                break;
            case E_Slider_Type.Vertical:
                nowValue = GUI.VerticalSlider(guiPos.Pos, nowValue, minValue, maxValue);
                break;
        }

        if(oldValue != nowValue)
        {
            changeValue?.Invoke(nowValue);
            oldValue = nowValue;
        }
        
    }

    protected override void StyleOnDraw()
    {
        switch (type)
        {
            case E_Slider_Type.Horizontal:
                nowValue = GUI.HorizontalSlider(guiPos.Pos, nowValue, minValue, maxValue, style, styleThumb);
                break;
            case E_Slider_Type.Vertical:
                nowValue = GUI.VerticalSlider(guiPos.Pos, nowValue, minValue, maxValue, style, styleThumb);
                break;
        }

        if (oldValue != nowValue)
        {
            changeValue?.Invoke(nowValue);
            oldValue = nowValue;
        }
    }
}
