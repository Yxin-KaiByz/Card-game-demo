
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
    private int page = 0;
    private List<Dictionary<string, string>> IDdata;
    private static CardLibrary instance;
    private int pagelimit;
    private Dictionary<string, string> cardData;    
    List<Librarycards> cards = new List<Librarycards>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cards.Add(transform.GetChild(i).gameObject.GetComponent<Librarycards>());
        }
    }


    public void Update()
    {
        GameConfigManager.Instance.Init();
        IDdata = GameConfigManager.Instance.GetCardLibraryLines();
        int size = IDdata.Count-1; // size = 7
        pagelimit = (size-1) / 4; // 4 cards per page; 
        print("size is " + size);
        print("pagelimit is "+ pagelimit);
        print("current page is "+ page);
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
                print("I am here");
                cards[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void BackToMenu()
    {
        SceneLoader.Instance.LoadScene("Menu");
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
