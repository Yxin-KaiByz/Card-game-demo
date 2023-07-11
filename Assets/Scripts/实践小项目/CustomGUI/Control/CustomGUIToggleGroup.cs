using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;

    //记录上一次为true的 toggle
    private CustomGUIToggle frontTurTog;

    void Start()
    {
        if (toggles.Length == 0)
            return;

        //通过遍历 来为多个 多选框 添加 监听事件函数
        //在函数中做 处理 
        //当一个为true时 另外两个变成false
        for (int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.changeValue += (value) =>
            {
                //当传入的 value  是ture时 需要把另外两个 
                //变成false
                if( value )
                {
                    //意味着另外两个要变成false
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        //这里有闭包  toggle就是上一个函数中申明的变量
                        //改变了它的生命周期
                        if( toggles[j] != toggle )
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    //记录上一次为true的toggle
                    frontTurTog = toggle;
                }
                //来判断 当前变成false的这个toggle是不是上一次为true
                //如果是 就不应该让它变成false
                else if( toggle == frontTurTog)
                {
                    //强制改成 true
                    toggle.isSel = true;
                }
            };
        }
    }

}
