using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ս��������
/// </summary>
public class FightManager : MonoBehaviour
{
    private static FightManager instance;
    public static FightManager Instance => instance;

    public FightUnit fightUnit;//ս����Ԫ

    public int MaxHp;//���Ѫ��
    public int CurHp; //��ǰѪ��

    public int MaxPointCount;//������(�������ĵ���)
    public int CurPointCount;//��ǰ����
    public int DefenseCount;//����ֵ

    public bool playerNewTurn;

    //��ʼ������
    public void Init()
    {
        MaxHp = PlayerPrefData.Instance.MaxHp;
        CurHp = PlayerPrefData.Instance.CurHp;
        CurPointCount = 4;
        DefenseCount = 10;
        MaxPointCount = 8;
        playerNewTurn = false;
    }

    private void Awake()
    {
        instance = this;
    }

    
    //change battle type
    public void ChnageType(E_FightType type)
    {
        switch (type)
        {
            case E_FightType.None:
                break;
            case E_FightType.Init:
                //�տ�ʼ����init����init�л��л���playerTurn
                fightUnit = new FightInit();
                break;
            case E_FightType.Player:
                fightUnit = new PlayerTurn();
                break;
            case E_FightType.Enemy:
                fightUnit = new EnemyTurn();
                break;
            case E_FightType.Win:
                fightUnit = new FightWin();
                break;
            case E_FightType.Loss:
                fightUnit = new FightLose();
                break;
        }
        
        fightUnit.Init();
    }

    //��������߼�
    public void GetPlayerHit(int hit)
    {
        //�ۻ���
        if (DefenseCount >= hit)
        {
            DefenseCount -= hit;
        }
        else
        {
            hit = hit - DefenseCount;
            DefenseCount = 0;
            CurHp -= hit;
            if (CurHp < 0) {

                CurHp = 0;
                //game over
                ChnageType(E_FightType.Loss);
            } 
            
        }
        //�������
        UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateHP();
        UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
       
    }

    public void Update()
    {
        if(fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }
}

//ս��ö��
public enum E_FightType { 
    None,
    Init,
    Player, //player turn
    Enemy, //enemy turn
    Win,
    Loss

}

