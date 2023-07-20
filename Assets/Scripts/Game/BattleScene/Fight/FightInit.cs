using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        EnemyManager.Instance.LoadRes("10001"); //���عؿ���1

        //�л�����һغ�
        FightManager.Instance.ChnageType(E_FightType.Player);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
