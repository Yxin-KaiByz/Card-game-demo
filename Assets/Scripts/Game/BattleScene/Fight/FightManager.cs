using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗管理器
/// </summary>
public class FightManager : MonoBehaviour
{
    private static FightManager instance;
    public static FightManager Instance => instance;

    public FightUnit fightUnit;//战斗单元

    public int MaxHp;//最大血量
    public int CurHp; //当前血量

    public int MaxPointCount;//最大点数(卡牌消耗点数)
    public int CurPointCount;//当前点数
    public int DefenseCount;//防御值

    public bool playerNewTurn;

    //初始化属性
    public void Init()
    {
        MaxHp = 10;
        CurHp = 10;
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
                //刚开始都是init，在init中会切换到playerTurn
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

    //玩家受伤逻辑
    public void GetPlayerHit(int hit)
    {
        //扣护盾
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
        //界面更新
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

//战斗枚举
public enum E_FightType { 
    None,
    Init,
    Player, //player turn
    Enemy, //enemy turn
    Win,
    Loss

}

