using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {
        
        Debug.Log("Init complete");

        //显示战斗界面
        UIMgr.Instance.ShowUI<FightUI>("FightUI");

        //初始化战斗卡牌
        FightCardManager.Instance.Init();

        //敌人生成
        EnemyManager.Instance.LoadRes("10003"); //加载关卡三1
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
