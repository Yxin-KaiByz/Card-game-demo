using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 敌人管理器
/// </summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();
    
    //存储战斗中的敌人
    private List<Enemy> enemyList;

    //加载敌人
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();
        //Id	Name	EnemyIds	           Pos
        //10003	 3	 10001=10002=10003  3,0,1=0,0,1=-3,0,1

        //读取关卡表
        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);

        //敌人id信息
        string[] enemyIds = levelData["EnemyIds"].Split('=');

        //敌人位置信息
        string[] enemyPos = levelData["Pos"].Split('=');

        for(int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            //敌人位置
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            //根据敌人id获取单个敌人信息
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);

            //从资源路径加载
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;
            Vector3 pos = new Vector3(x, y, z);
            obj.GetComponent<RectTransform>().localPosition = pos;
            /*Transform canvas = GameObject.Find("Canvas").transform;
            Texture2D enemy = Resources.Load<Texture2D>("Sprites/Character/cheche_bgremoved");*/

            //加战斗敌人脚本
            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData);//存储信息
            enemyList.Add(enemy);//加入list

        }

    }

    //移除敌人
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //后续做是否击杀所有怪物判断
        if(enemyList.Count == 0)
        {
            FightManager.Instance.ChnageType(E_FightType.Win);
        }
    }

    /*//remove enemy
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //后续左是否击杀所有怪物判断
    }*/

    //执行所有活着的怪物的行为
    public IEnumerator DoAllEnemyAction()
    {
        for(int i = 0; i < enemyList.Count; i++)
        {
            //延迟，先后执行
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //行动完成后更新所有敌人行为
        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //切换到玩家回合
        FightManager.Instance.ChnageType(E_FightType.Player);
        FightManager.Instance.playerNewTurn = true;

    }
}
