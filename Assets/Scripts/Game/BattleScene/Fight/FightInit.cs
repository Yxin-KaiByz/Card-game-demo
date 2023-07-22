using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FightInit : FightUnit
{
    public override void Init()
    {
        
        //��ʼ��ս����ֵ
        FightManager.Instance.Init();

        Debug.Log("Init complete");

        //��ʾս������
        UIMgr.Instance.ShowUI<FightUI>("FightUI");

        //��ʼ��ս������
        FightCardManager.Instance.Init();

        //��������
        //���һ��ֵ
        string randomLevel = "10001";
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                randomLevel = "10001";
                break;
            case 1:
                randomLevel = "10002";
                break;
            case 2:
                randomLevel = "10003";
                break;
            default:
                break;
        }
        

        EnemyManager.Instance.LoadRes(randomLevel); //���عؿ���1

        //�л�����һغ�
        FightManager.Instance.ChnageType(E_FightType.Player);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
