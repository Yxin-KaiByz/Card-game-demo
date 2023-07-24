using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//ѡ�����Ŀ���
public class SelectingCard : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        
        
        //���뱳��
        PlayerBag.Instance.addCard(gameObject.name.Split("(")[0]);
        print(gameObject.name.Split("(")[0]);
        if(PlayerObject.currentLevel != "Normal")
        {
            //������boss��Ҫ��map
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
        //�����Ŵ�1.5ÿ������0.25
        transform.DOScale(1.5f, 0.25f);
        //ͨ���߿��������ɫ
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //�ص�1
        transform.DOScale(1, 0.25f);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

   
}
