using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerObject : RoleObject
{
    private static PlayerObject instance;
    public static PlayerObject Instance => instance;

    public static string modelPath;
    private Collider2D playerCollider;
    private static GameObject collideObject;
    private static bool finishBattle = false;
    private FightingObject fightingObject;
    public static int NUMBER_OF_DESTORIED;
    public static string currentLevel;

    // Start is called before the first frame update
    protected override void Awake()
    {
        instance = this;
        currentLevel = "Normal";
        fightingObject = PlayerPrefsDataMgr.Instance.LoadData(typeof(FightingObject), "FightingWith" + NUMBER_OF_DESTORIED) as FightingObject;
        //父类相关的Awake逻辑一定概要保留
        base.Awake();
        playerCollider = GetComponent<Collider2D>();
        //选择对应的玩家model预设体并挂载与Player
        //loadCharModel(characterData.Instance.characterID);
        loadCharModel(0);
        //开启输入控制
        InputMgr.GetInstance().StartOrEndCheck(true);
        //获取输入权限
        GetController();
        if (finishBattle)
        {
            /*if (collideObject == null)
            {
                print("is null");
            }
            else
            {
                print("Not null");
            }*/
            //根据一共打败了几个敌人，来找到playerpref之前存储的摧毁过的敌人，重新一起摧毁
            for(int i = 0; i < NUMBER_OF_DESTORIED;  i++)
            {
                FightingObject temp = PlayerPrefsDataMgr.Instance.LoadData(typeof(FightingObject), "FightingWith" + i) as FightingObject;
                //改变人物位置
                if (i ==  NUMBER_OF_DESTORIED - 1)
                {
                    transform.position = new Vector3(temp.currentX, transform.position.y, transform.position.z);
                }
                string name = temp.fightingName;
                print("tring to destory " + name);
                GameObject obj = GameObject.Find(name);
                if (obj != null)
                {
                    Destroy(obj);
                    EnemySpawnPoint.Instance.spawnPointCollider.Remove(obj.GetComponent<Collider2D>());
                    //print(EnemySpawnPoint.Instance.spawnPointCollider.Count);
                }
                else
                {
                    print("why is null");
                }
            }
            
            //GameObject temp = GameObject.Find((PlayerPrefsDataMgr.Instance.LoadData(typeof(FightingObject), "FightingWith") as FightingObject).fightingName);
            /*print(temp.name);
            Destroy(temp);*/
            finishBattle = false;

        }
        PlayerBag bag = PlayerBag.Instance;
        bag.addCardIntoBag();
    }

    /*private void Start()
    {
        if (finishBattle)
        {
            Destroy(playerCollider);
        }
    }*/

    public static void changeFinish()
    {
        finishBattle = true;
    }

    /*public static GameObject getCollideObject()
    {
        return collideObject;
    }*/





    protected override void Update()
    {
        //print(finishBattle);
        //一定要保持这个base.Update的存在 因为 移动逻辑 是写在父类中的
        //除非之后你要重写 才不需要它
        base.Update();
        //if (playerCollider.IsTouching(EnemySpawnPoint.Instance.spawnPointCollider))
        bool touchingOrNot = false;
        Collider2D collider = null;
        string collidingName = null;
        
        //检查是否和碰撞体发生碰撞，如果有就改变prompt的位置
        foreach(Collider2D each in EnemySpawnPoint.Instance.spawnPointCollider){
            if (playerCollider.IsTouching(each))
            {
                touchingOrNot = true;
                EnemySpawnPoint.Instance.promptSprite.GetComponent<RectTransform>().position = each.GetComponent<Transform>().position;
                collidingName = each.name;
                collider = each;
                break;
            }
            
        }
        GameObject boss = GameObject.Find("Boss");
        if (boss != null && currentLevel != "Normal")
        {
            
            if (playerCollider.IsTouching(boss.GetComponent<Collider2D>()))
            {
                touchingOrNot = true;
                EnemySpawnPoint.Instance.promptSprite.GetComponent<RectTransform>().position = boss.GetComponent<Transform>().position;
                collidingName = boss.name;
                collider = boss.GetComponent<Collider2D>();
            }
        }

        if (touchingOrNot)
        {
            //print("Touching enemy");

            EnemySpawnPoint.Instance.promptSprite.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                finishBattle = false;
                //collideObject = EnemySpawnPoint.Instance.spawnedObject;
                /*if(collideObject == null)
                {
                    print("wtfwtf");
                }
                else
                {
                    print(collideObject.name);
                }
                DontDestroyOnLoad(GameObject.Find("LevelBackground"));*/
                print(collidingName);
                //设置碰撞物体和碰撞位置方便改变人物位置当战斗结束
                fightingObject.fightingName = collidingName;
                fightingObject.currentX = collider.transform.position.x;
                //持久化打了几个怪
                PlayerPrefsDataMgr.Instance.SaveData(fightingObject, "FightingWith" + NUMBER_OF_DESTORIED);
                FightingObject temp = PlayerPrefsDataMgr.Instance.LoadData(typeof(FightingObject), "FightingWith" + NUMBER_OF_DESTORIED) as FightingObject;
                
                print("I stored " + temp.fightingName);
                EnemySpawnPoint.Instance.spawnPointCollider.Remove(collider);
                NUMBER_OF_DESTORIED++;
                //更改txt数据
                /*Dictionary<string , string> data = GameConfigManager.Instance.GetSceneTypeById("1001");
                if(currentLevel == "Normal")
                {
                    data["NormalEnemyAmount"] = (int.Parse(data["NormalEnemyAmount"]) - 1).ToString();
                    print("after change the normal enemy amount is : " + data["NormalEnemyAmount"]);
                }*/
                //拿去数据
                LevelPlayerPref levelData = PlayerPrefsDataMgr.Instance.LoadData(typeof(LevelPlayerPref), "LevelPref") as LevelPlayerPref;
                if (currentLevel == "Normal")
                {
                    levelData.normalAmount -= 1;
                    print("after change the normal enemy amount is : " + levelData.normalAmount);
                }
                PlayerPrefsDataMgr.Instance.SaveData(levelData, "LevelPref");

                //随机load一种场景
                //Set to battle entrence
                SceneLoader.Instance.setTransition(2);
                SceneLoader.Instance.LoadScene("BattleScene");
                // SceneManager.LoadScene("BattleScene");
                return;
                
            }
        } else
        {
            EnemySpawnPoint.Instance.promptSprite.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 通过来自menu的CharID加载角色预设体，并挂载为子对象
    /// </summary>
    /// <param name="CharID"></param>
    public void loadCharModel(int CharID)
    {
        //string modelPath;
        switch (CharID)
        {
            case 0:
                modelPath = "Model/Bronya";
                break;
            case 1:
                modelPath = "Model/Elysia";
                break;
            case 2:
                modelPath = null;
                Debug.Log("Unimplemented charID 2");
                break;
            default:
                modelPath = null;
                Debug.Log("Error during loadCharModel, charID unknown");
                break;
        }
       
        GameObject playerModel = (GameObject)Resources.Load(modelPath);
        playerModel = Instantiate(playerModel);
        playerModel.transform.SetParent(transform, false);
        playerModel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    /// <summary>
    /// 给予控制权
    /// </summary>
    public void GetController()
    {
        //事件中心 有加 就有减 一定不要往里面传 那么大表达式 一定是在下方去声明一个函数
        EventCenter.GetInstance().AddEventListener<float>("Horizontal", CheckX);
        EventCenter.GetInstance().AddEventListener<float>("Vertical", CheckY);
        //监听按键按下内容
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
    }

    /// <summary>
    /// 剥夺控制权
    /// </summary>
    public void RemoveController()
    {
        //事件中心 有加 就有减 一定不要往里面传 那么大表达式 一定是在下方去声明一个函数
        EventCenter.GetInstance().RemoveEventListener<float>("Horizontal", CheckX);
        EventCenter.GetInstance().RemoveEventListener<float>("Vertical", CheckY);
        //监听按键按下内容
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
    }

    private void CheckX(float x)
    {
        //x 就会是 -1 0 1三个值的数 
        // 按 A 为-1  不按为0  按D为1
        //获取横向输入方向 
        moveDir.x = x;
    }

    private void CheckY(float y)
    {
        //x 就会是 -1 0 1三个值的数 
        // 按 S 为-1  不按为0  按W为1
        //获取纵向输入方向
        moveDir.y = y;
    }

    /// <summary>
    /// 检测玩家 除移动意外的 输入内容
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyDown(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.J:
                print("J键");
                break;
            case KeyCode.K:
                print("K键");
                break;
            case KeyCode.L:
                print("L键");
                break;
            case KeyCode.Space:
                print("Space键");
                break;
        }
    }

    


    private void OnDestroy()
    {
        //事件 有加就有减 移除时一定要注销事件
        RemoveController();
    }
}
