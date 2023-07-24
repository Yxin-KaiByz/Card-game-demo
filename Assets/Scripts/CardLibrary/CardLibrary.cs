
using Assets.Scripts.CardLibrary;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CardLibrary : MonoBehaviour
{    
    public static CardLibrary Instance => instance;
    private static CardLibrary instance;
    List<Dictionary<string, string>> AllCardPair;
    private int page = 0;
    private int pagelimit;
    private List<Dictionary<string, string>> IDdata;
    private Dictionary<string, string> cardData;    
    List<Librarycards> cards = new List<Librarycards>();

    private void Start()
    {
        GameConfigManager.Instance.Init();
        for (int i = 0; i < transform.childCount; i++)
        {
            cards.Add(transform.GetChild(i).gameObject.GetComponent<Librarycards>());
        }
        IDdata = GameConfigManager.Instance.GetCardLibraryLines();
        AllCardPair = GameConfigManager.Instance.GetCardLines();
    }

    // 根据卡牌ID获取卡牌信息，并且生成卡牌
    public void Update()
    {
        int size = IDdata.Count; 
        pagelimit = (size-1) / 4; // 4 cards per page; 
        for(int i = 0; i < 4; i++)
        {
            if (page* 4 + i < size)
            {
                cards[i].gameObject.SetActive(true);
                Dictionary<string,string> tempData = IDdata[page * 4 + i];
                String cardID = tempData["Id"];
                cardData = GameConfigManager.Instance.GetCardById(cardID);
                cards[i].Init(cardData);
            }
            else
            {
                cards[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void AttackCards()
    {
        List<Dictionary<string, string>> AttackCards = new List<Dictionary<string, string>>();
        
        for(int i = 0;i < AllCardPair.Count; i++)
        {
            if (AllCardPair[i]["Type"] == "10001")
            {
                AttackCards.Add(AllCardPair[i]);
            }
        }
        IDdata = AttackCards;
        return;
    }

    public void DefenseCards()
    {
        List<Dictionary<string, string>> DefenseCards = new List<Dictionary<string, string>>();
        for (int i = 0; i < AllCardPair.Count; i++)
        {
            if (AllCardPair[i]["Type"] == "10002")
            {
                DefenseCards.Add(AllCardPair[i]);
            }
        }
        IDdata = DefenseCards;
        return;
    }

    public void DrinkCards()
    {
        List<Dictionary<string, string>> DrinkCards = new List<Dictionary<string, string>>();
        for (int i = 0; i < AllCardPair.Count; i++)
        {
            if (AllCardPair[i]["Type"] == "10003")
            {
                DrinkCards.Add(AllCardPair[i]);
            }
        }
        IDdata = DrinkCards;
        return;
    }

    public void BackToMenu()
    {
        SceneLoader.instance.LoadScene("Menu");
    }
    public void next()
    {
    if (page < pagelimit)
        {
            page++;
        }
    }

    public void last()
    {
        if (page > 0)
        {
            page--;
        }
        
    }
}
