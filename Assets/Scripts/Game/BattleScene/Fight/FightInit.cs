using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FightInit : FightUnit
{
    public override void Init()
    {
        
        //初始化战斗数值
        FightManager.Instance.Init();

        Debug.Log("Init complete");

        //显示战斗界面
        UIMgr.Instance.ShowUI<FightUI>("FightUI");

        //初始化战斗卡牌
        FightCardManager.Instance.Init();

        //敌人生成
        //随机一个值
        string randomLevel = "10001";
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                randomLevel = "10001";
                break;
            case 1:
                randomLevel = "10002";
                break;
            case 2:
                randomLevel = "10003";
                break;
            default:
                break;
        }
        

        EnemyManager.Instance.LoadRes(randomLevel); //加载关卡三1

        //切换到玩家回合
        FightManager.Instance.ChnageType(E_FightType.Player);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
