using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗卡牌管理器
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList;//卡堆集合
    public List<string> usedCardList;//弃牌堆
    //初始化
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //定义临时集合
        List<string> tempList = new List<string>();
        //将玩家卡牌放到临时集合
        tempList.AddRange(RoleManager.Instance.cardList);

        while(tempList.Count > 0)
        {
            //随机下标
            int temIndex = Random.Range(0, tempList.Count);

            //添加到卡堆
            cardList.Add(tempList[temIndex]);
            

            //remove from temp
            tempList.RemoveAt(temIndex);
        }
        Debug.Log(cardList.Count);
    }
}
