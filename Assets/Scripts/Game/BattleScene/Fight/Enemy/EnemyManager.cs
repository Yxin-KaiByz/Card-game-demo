using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ���˹�����
/// </summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();
    
    //�洢ս���еĵ���
    private List<Enemy> enemyList;

    //���ص���
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();
        //Id	Name	EnemyIds	           Pos
        //10003	 3	 10001=10002=10003  3,0,1=0,0,1=-3,0,1

        //��ȡ�ؿ���

        //��ȡĿǰ���ĸ�map
        string mapId = MainLevelManager.levelId;
        Dictionary<string, string> dic = GameConfigManager.Instance.GetSceneTypeById(mapId);
        string mapStoredLocation = dic["NormalLevelEnemyLocation"];
        UnityEngine.TextAsset textAsset = Resources.Load<UnityEngine.TextAsset>(mapStoredLocation);
        //�������map�ҵ���Źؿ���locationȻ�����֮ǰ�����������ֵ��ȡ�ؿ���Ϣ
        GameConfigData levelConfigData = new GameConfigData(textAsset.text);
        Dictionary<string, string> levelData = levelConfigData.GetOneById(id);
        //Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);

        //����id��Ϣ
        string[] enemyIds = levelData["EnemyIds"].Split('=');
        

        //����λ����Ϣ
        string[] enemyPos = levelData["Pos"].Split('=');

        //�����boss�ؿ�
        if (PlayerObject.currentLevel != "Normal")
        {
            if(MainLevelManager.levelId == "1002")
            {
                enemyIds = levelData["Boss2"].Split('=');
                enemyPos = levelData["BossPos2"].Split('=');
            }
            else
            {
                enemyIds = levelData["Boss"].Split('=');
                enemyPos = levelData["BossPos"].Split('=');
            }
            
        }

        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            //����λ��
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            //���ݵ���id��ȡ����������Ϣ
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);

            //����Դ·������
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;
            Vector3 pos = new Vector3(x, y, z);
            obj.GetComponent<RectTransform>().localPosition = pos;
            /*Transform canvas = GameObject.Find("Canvas").transform;
            Texture2D enemy = Resources.Load<Texture2D>("Sprites/Character/cheche_bgremoved");*/

            //��ս�����˽ű�
            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData);//�洢��Ϣ
            enemyList.Add(enemy);//����list

        }

    }

    //�Ƴ�����
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //�������Ƿ��ɱ���й����ж�
        if(enemyList.Count == 0)
        {
            FightManager.Instance.ChnageType(E_FightType.Win);
        }
    }

    /*//remove enemy
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //�������Ƿ��ɱ���й����ж�
    }*/

    //ִ�����л��ŵĹ������Ϊ
    public IEnumerator DoAllEnemyAction()
    {
        for(int i = 0; i < enemyList.Count; i++)
        {
            //�ӳ٣��Ⱥ�ִ��
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //�ж���ɺ�������е�����Ϊ
        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //�л�����һغ�
        FightManager.Instance.ChnageType(E_FightType.Player);
        FightManager.Instance.playerNewTurn = true;

    }
}
