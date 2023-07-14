using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleApp : MonoBehaviour
{
    public string baseBackground;
   
    // Start is called before the first frame update
    void Start()
    {
        //初始化配置表，会装在好所有的data
        //里面有封装了方法获取每个table的数值
        //可以根据id获取一行或者整个table
        //因为init中会根据路径所给的txt文件转化为string去
        //instantiate GameConfigData，在实例化的过程中
        //就会把表的所有信息利用键值对存在list里面
        //我们可以通过id去区分他们
        GameConfigManager.Instance.Init();

        //通过背景名显示背景
        UIMgr.Instance.ShowUI<UIBackground>(baseBackground);
        
        //test, 获取id为1001的卡牌的名字
        string name = GameConfigManager.Instance.GetCardById("1001")["Name"];
        print(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
