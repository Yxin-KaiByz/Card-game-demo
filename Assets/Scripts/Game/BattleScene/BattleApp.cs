using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleApp : MonoBehaviour
{
    private static BattleApp instance;
    public static BattleApp Instance => instance;
    public string baseBackground;
   
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ�����ñ���װ�ں����е�data
        //�����з�װ�˷�����ȡÿ��table����ֵ
        //���Ը���id��ȡһ�л�������table
        //��Ϊinit�л����·��������txt�ļ�ת��Ϊstringȥ
        //instantiate GameConfigData����ʵ�����Ĺ�����
        //�ͻ�ѱ��������Ϣ���ü�ֵ�Դ���list����
        //���ǿ���ͨ��idȥ��������
        baseBackground = "Ice2Prefab";
        GameConfigManager.Instance.Init();

        //ͨ����������ʾ����
        UIMgr.Instance.ShowUI<UIBackground>(baseBackground);
        
        //��ʼ���û���Ϣ
        RoleManager.Instance.Init();


        //test, ��ȡidΪ1001�Ŀ��Ƶ�����
        string name = GameConfigManager.Instance.GetCardById("1001")["Name"];
        print(name);

        //test2
        string name2 = GameConfigManager.Instance.GetCardById("1002")["Des"];
        print(name2);

        //��ʼ��ս������
        FightManager.Instance.ChnageType(E_FightType.Init);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
