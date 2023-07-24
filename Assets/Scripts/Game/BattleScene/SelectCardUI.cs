using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.EventSystems;


/// <summary>
/// ����ҳ��
/// </summary>
public class SelectCardUI : UIBase
{
    public static GameObject collideObject;

    private void Start()
    {
        transform.Find("bg").transform.Find("content").GetComponent<Transform>().DOLocalMoveY(0, 0.5f);

        transform.Find("bg").transform.Find("content").transform.Find("returnBtn").GetComponent<Button>().onClick.AddListener(EndBattle);
        //transform.Find("returnBtn").GetComponent<Button>().onClick.AddListener(EndBattle);
        transform.Find("bg").transform.Find("content").transform.Find("grid").transform.Find("addHP").GetComponent<Button>().onClick.AddListener(addHP);
        transform.Find("bg").transform.Find("content").transform.Find("grid").transform.Find("selectCard").GetComponent<Button>().onClick.AddListener(selectCard);
    }

    private void EndBattle()
    {
        //Destroy(PlayerObject.Instance.collideObject);
        /*Destroy(RoleManager.Instance.player);
        Destroy(GameObject.Find("Canvas").transform.Find("Ice2Prefab"));
        Destroy(GameObject.Find("Canvas").transform.Find("FightUI"));
        Destroy(GameObject.Find("Music"));

        Destroy(this);*/
        //print("Try to end Battle");
        
        //SceneLoader.Instance.LoadScene("Main");
        /*SceneManager.LoadScene("Main");
        PlayerObject.changeFinish();*/

        if (PlayerObject.currentLevel != "Normal")
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

    private void addHP()
    {
        //print("try to add Hp");
        PlayerPrefData.instance.MaxHp += 5;
        PlayerPrefData.instance.CurHp += 5;
        SceneManager.LoadScene("Main");
        PlayerObject.changeFinish();
    }

    private void selectCard()
    {
        List<Dictionary<string, string>> cardList = GameConfigManager.Instance.GetCardLines();
        //�������������ȥ����
        List<int> ints = new List<int>();
        int rand = Random.Range(0, cardList.Count);
        //����ظ���rollһ��
        for(int i = 0; i < 3; i++)
        {
            while (ints.Contains(rand))
            {
                rand = Random.Range(0, cardList.Count);
            }
            ints.Add(rand);
        }
        //�������������index�����Ӧ��dictionary
        List<Dictionary<string, string>> randomRolledCard = new List<Dictionary<string, string>>();
        for(int i = 0;i < 3;i++)
        {
            randomRolledCard.Add(cardList[ints[i]]);
        }

        print(randomRolledCard[0]["Id"]);

        //ʧ������
        transform.Find("bg").transform.Find("content").gameObject.SetActive(false);

        //��ʼ�����ſ���
        GameObject cardOne = Instantiate(Resources.Load("Model/Cards/" + randomRolledCard[0]["Id"]) as GameObject, transform.Find("bg"));
        GameObject cardTwo = Instantiate(Resources.Load("Model/Cards/" + randomRolledCard[1]["Id"]) as GameObject, transform.Find("bg"));
        GameObject cardThree = Instantiate(Resources.Load("Model/Cards/" + randomRolledCard[2]["Id"]) as GameObject, transform.Find("bg"));
        //����ű�
        cardOne.AddComponent<SelectingCard>();
        cardTwo.AddComponent<SelectingCard>();
        cardThree.AddComponent<SelectingCard>();
        //�ı�λ��
        cardOne.transform.GetComponent<RectTransform>().localPosition = new Vector3(-529, 136, 0);
        cardTwo.transform.GetComponent<RectTransform>().localPosition = new Vector3(41, 136, 0);
        cardThree.transform.GetComponent<RectTransform>().localPosition = new Vector3(611, 136, 0);
        cardOne.transform.GetComponent<RectTransform>().localScale = new Vector3(1.35f, 1.39f, 1.56f);
        cardTwo.transform.GetComponent<RectTransform>().localScale = new Vector3(1.35f, 1.39f, 1.56f);
        cardThree.transform.GetComponent<RectTransform>().localScale = new Vector3(1.35f, 1.39f, 1.56f);

        

        //transform.Find("returnBtn").gameObject.SetActive(true);



    }

    
}
