using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag
{
    public int golds;
    public int DEFAULT_ATTACK = 4;
    public List<string> cardList = new List<string>();
    private static PlayerBag instance;

    private PlayerBag() {

        if (cardList.Count == 0)
        {
            //初始四张攻击卡,四张防御
            for (int i = 0; i < DEFAULT_ATTACK; i++)
            {
                cardList.Add("1000");
                cardList.Add("1001");
            }
            //两张效果卡
            cardList.Add("1002");
            cardList.Add("1002");
        }


    }

    //singleton pattern
    public static PlayerBag Instance
    {
        get{ 
            if(instance == null)
            {
                instance = new PlayerBag(); 
            }
            
            return instance;
        }
        
    }


    public void addCard(string id)
    {
        cardList.Add(id);
    }

    public void addCardIntoBag()
    {
        GameObject grid = GameObject.Find("Grids");
        List<string> list = new List<string>();
        for(int i = 0; i < cardList.Count;i++)
        {
            if (!list.Contains(cardList[i]))
            {
                list.Add(cardList[i]);
                GameObject card = GameObject.Instantiate(Resources.Load("Model/Cards/" + cardList[i]) as GameObject, grid.transform);
                card.transform.GetComponent<RectTransform>().localScale = new Vector3(0.0046f, 0.00475f, 0.00535f);
            }
            
        }
        
    }

    

   
}
