using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ½±ÀøÒ³Ãæ
/// </summary>
public class SelectCardUI : UIBase
{
    public static GameObject collideObject;

    private void Start()
    {
        transform.Find("bg").transform.Find("content").GetComponent<Transform>().DOLocalMoveY(0, 0.5f);

        transform.Find("bg").transform.Find("content").transform.Find("returnBtn").GetComponent<Button>().onClick.AddListener(EndBattle);
    }

    private void EndBattle()
    {
        //Destroy(PlayerObject.Instance.collideObject);
        /*Destroy(RoleManager.Instance.player);
        Destroy(GameObject.Find("Canvas").transform.Find("Ice2Prefab"));
        Destroy(GameObject.Find("Canvas").transform.Find("FightUI"));
        Destroy(GameObject.Find("Music"));

        Destroy(this);*/
        print("Try to end Battle");
        
        //SceneLoader.Instance.LoadScene("Main");
        SceneManager.LoadScene("Main");
        PlayerObject.changeFinish();
        
    }

}
