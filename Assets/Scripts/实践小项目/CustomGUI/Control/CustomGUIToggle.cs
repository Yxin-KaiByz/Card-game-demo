using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIToggle : CustomGUIControl
{
    public bool isSel;

    public event UnityAction<bool> changeValue;

    private bool isOldSel;

    protected override void StyleOffDraw()
    {
        isSel = GUI.Toggle(guiPos.Pos, isSel, content);
        //只有变化时 才告诉外部 执行函数 否则 没有必要一直告诉别人同一个值
        if(isOldSel != isSel)
        {
            changeValue?.Invoke(isSel);
            isOldSel = isSel;
        }
    }

    protected override void StyleOnDraw()
    {
        isSel = GUI.Toggle(guiPos.Pos, isSel, content, style);
        //只有变化时 才告诉外部 执行函数 否则 没有必要一直告诉别人同一个值
        if (isOldSel != isSel)
        {
            changeValue?.Invoke(isSel);
            isOldSel = isSel;
        }
    }
}
