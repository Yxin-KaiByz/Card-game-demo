using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//��������û��Ŀ�����Ϣ��ҵȵ�
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();
    public static int DEFAULT_ATTACK = 4;
    public List<string> cardList;//�洢ӵ�еĿ���
    public string playerObjectLocation;
    public GameObject player;
    public PlayerBag bag;

    public void Init()
    {
        //�õ����ģ�ͣ����볡����
        playerObjectLocation = PlayerObject.modelPath;

        player = Object.Instantiate(Resources.Load(playerObjectLocation)) as GameObject;
        player.transform.position = new Vector3(415.36f,545.69f,0);
        player.transform.GetComponentInChildren<Transform>().DOScale(new Vector3(124, 124, 1),0);
        
        cardList = new List<string>();
        bag = PlayerBag.Instance;


        /*//��ʼ���Ź�����,���ŷ���
        for(int i = 0; i < DEFAULT_ATTACK; i++)
        {
            cardList.Add("1000");
            cardList.Add("1001");
        }
        //����Ч����
        cardList.Add("1002");
        cardList.Add("1002");*/
        List<string> bagCard = bag.cardList;
        for(int i = 0; i < bagCard.Count; i++)
        {
            cardList.Add(bagCard[i]); 
        }
        


    }

 
}
