using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//负责管理用户的卡牌信息金币等等
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();
    public static int DEFAULT_ATTACK = 4;
    public List<string> cardList;//存储拥有的卡牌
    public string playerObjectLocation;
    public GameObject player;

    public void Init()
    {
        //得到玩家模型，放入场景中
        playerObjectLocation = PlayerObject.modelPath;

        player = Object.Instantiate(Resources.Load(playerObjectLocation)) as GameObject;
        player.transform.position = new Vector3(415.36f,545.69f,0);
        player.transform.GetComponentInChildren<Transform>().DOScale(new Vector3(124, 124, 1),0);
        
        cardList = new List<string>();
        //初始四张攻击卡,四张防御
        for(int i = 0; i < DEFAULT_ATTACK; i++)
        {
            cardList.Add("1000");
            cardList.Add("1001");
        }
        //两张效果卡
        cardList.Add("1002");
        cardList.Add("1002");

    }

 
}
