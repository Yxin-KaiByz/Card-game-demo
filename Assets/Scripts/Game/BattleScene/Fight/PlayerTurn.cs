using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : FightUnit
{
    public override void Init()
    {
        UIMgr.Instance.ShowTip("Player Turn", Color.green, delegate ()
        {
            //如果卡堆没有卡，重新初始化
            if(FightCardManager.Instance.HasCard() == false)
            {
                FightCardManager.Instance.Init();
                //更新弃卡堆数量
                UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardHeapCount();
            }

            //抽牌
            Debug.Log("抽牌");
            UIMgr.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4); //抽张
            UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
            //更新点数
            if (FightManager.Instance.playerNewTurn)
            {
                FightManager.Instance.CurPointCount += 5;
                UIMgr.Instance.GetUI<FightUI>("FightUI").UpdatePoint();
                FightManager.Instance.playerNewTurn = false;
            }

            //更新卡牌数量
            UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        });
    }

    public override void OnUpdate()
    {
       
    }
}
