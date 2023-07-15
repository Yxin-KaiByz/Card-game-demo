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

    private void Awake()
    {
        instance = this;
    }

    public void ChnageType(E_FightType type)
    {
        switch (type)
        {
            case E_FightType.None:
                break;
            case E_FightType.Init:
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

