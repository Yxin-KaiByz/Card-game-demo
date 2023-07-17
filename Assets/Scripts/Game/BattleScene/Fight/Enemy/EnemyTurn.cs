using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : FightUnit
{
    public override void Init()
    {
        //删除所有卡牌
        UIMgr.Instance.GetUI<FightUI>("FightUI").RemoveAllCard();
        //显示敌人回合提示
        UIMgr.Instance.ShowTip("Enemy Turn", Color.red, delegate ()
        {

            
            Debug.Log("执行敌人AI");
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        });

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
