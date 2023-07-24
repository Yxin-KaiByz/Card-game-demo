using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//选奖励的卡牌
public class SelectingCard : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        
        
        //放入背包
        PlayerBag.Instance.addCard(gameObject.name.Split("(")[0]);
        print(gameObject.name.Split("(")[0]);
        if(PlayerObject.currentLevel != "Normal")
        {
            //打完了boss关要换map
            int curMapId = int.Parse(MainLevelManager.levelId);
            curMapId += 1;
            MainLevelManager.levelId = curMapId.ToString();
            MainLevelManager.isNormal = true;
            MainLevelManager.NormalAmount = 3;
            MainLevelManager.BossAmount = 1;
            PlayerObject.NUMBER_OF_DESTORIED = 0;
            SceneManager.LoadScene("Main");
            //PlayerObject.changeFinish();
        }
        else
        {
            SceneManager.LoadScene("Main");
            PlayerObject.changeFinish();
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //慢慢放大到1.5每次增加0.25
        transform.DOScale(1.5f, 0.25f);
        //通过边框给卡牌上色
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //回到1
        transform.DOScale(1, 0.25f);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

   
}
