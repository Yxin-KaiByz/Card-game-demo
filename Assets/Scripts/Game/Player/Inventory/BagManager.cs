using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.EventSystems;

//控制背包面板下移和恢复
public class BagManager : MonoBehaviour
{
    public Button closeButton;
    public Button openButton;
    public void Start()
    {
        closeButton = gameObject.transform.Find("Bag").GetComponentInChildren<Button>();
      /*  closeButton.onClick.AddListener(() =>
        {
            CloseBag();
        });*/

        openButton = gameObject.transform.Find("OpenBag").GetComponent<Button>();
/*
        openButton.onClick.AddListener(() =>
        {
            OpenBag();
        });*/

        //ExecuteEvents.Execute(openButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        //ExecuteEvents.Execute(closeButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    public void OpenBag()
    {
        gameObject.transform.Find("Bag").DOMoveY((-8.74f), 1f);
    }

    public void CloseBag()
    {
        gameObject.transform.Find("Bag").DOLocalMoveY(-188, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void print123()
    {
        print("12312312321");
    }
}
