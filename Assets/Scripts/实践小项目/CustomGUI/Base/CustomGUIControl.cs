using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Style_OnOff
{
    On,
    Off,
}

public abstract class CustomGUIControl : MonoBehaviour
{
    //提取控件的共同表现
    //位置信息
    public CustomGUIPos guiPos;
    //显示内容信息
    public GUIContent content;
    //自定义样式
    public GUIStyle style;
    //自定义样式是否启用的开关
    public E_Style_OnOff styleOnOrOff = E_Style_OnOff.Off;

    //提供给外部 绘制GUI控件的方法
    public void DrawGUI()
    {
        switch (styleOnOrOff)
        {
            case E_Style_OnOff.On:
                StyleOnDraw();
                break;
            case E_Style_OnOff.Off:
                StyleOffDraw();
                break;
        }
    }

    /// <summary>
    /// 自定义样式开启时 的绘制方法
    /// </summary>
    protected abstract void StyleOnDraw();


    /// <summary>
    /// 自定义样式关闭时 的绘制方法
    /// </summary>
    protected abstract void StyleOffDraw();
}
