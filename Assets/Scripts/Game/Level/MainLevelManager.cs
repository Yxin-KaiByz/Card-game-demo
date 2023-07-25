using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 负责管理main的场景
/// </summary>
public class MainLevelManager : MonoBehaviour
{
    private static MainLevelManager instance;
    public static MainLevelManager Instance => instance;
    public static string levelId = "1001";
    public string levelMapLocation;
    public Sprite image;
    public Dictionary<string, string> sceneDic;
    public List<Dictionary<string, string>> sceneMap;
    public static int NormalAmount;
    public static int BossAmount;
    private GameObject teleport;
    public GameObject spawnOne;
    public GameObject spawnTwo;
    public GameObject spawnThree;
    public static bool isNormal;
    

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        //levelId = "1002";

        GameConfigManager.Instance.Init();
        sceneDic = GameConfigManager.Instance.GetSceneTypeById(levelId);
        spawnOne = Instantiate(Resources.Load(sceneDic["SpawnOne"]) as GameObject, GameObject.Find("SpawnObject").transform);
        spawnOne.transform.position += new Vector3(0, 1, 0);
        spawnTwo = Instantiate(Resources.Load(sceneDic["SpawnTwo"]) as GameObject, GameObject.Find("SpawnObject2").transform);
        spawnTwo.transform.position += new Vector3(0, 1, 0);
        spawnThree = Instantiate(Resources.Load(sceneDic["SpawnThree"]) as GameObject, GameObject.Find("SpawnObject3").transform);
        spawnThree.transform.position += new Vector3(0, 0, 0);
        //used for debug
        /*if (sceneDic == null) { 
            print("Is null");
            sceneMap = GameConfigManager.Instance.GetSceneLines();
            for (int i = 0; i < sceneMap.Count; i++)
            {
                Dictionary<string, string> dic = sceneMap[i];
                foreach (KeyValuePair<string, string> pair in dic)
                {
                    if (dic["Id"]  == levelId)
                    {
                        print("I find id");
                    }
                    else
                    {
                        print(dic["Id"]);
                    }
                    Debug.Log(pair.Key + " and " + pair.Value);
                }
            }

        }
        else
        {
           
            foreach (KeyValuePair<string, string> pair in sceneDic)
            {
                Debug.Log( pair.Key + " " + pair.Value);
            }

            
        }*/
        levelMapLocation = sceneDic["Location"];
        //初始化游戏数据
        LevelPlayerPref levelData = PlayerPrefsDataMgr.Instance.LoadData(typeof(LevelPlayerPref), "LevelPref") as LevelPlayerPref;

        //如果没有数据要么是false要么是0
        //再加一个判断是因为每次重启static值会刷新但是持久化的值不会所以防止normalAmount重启游戏一直是0
        if (!levelData.notFirst || (PlayerObject.NUMBER_OF_DESTORIED == 0))
        {
            levelData.notFirst = true;
            levelData.normalAmount = int.Parse(sceneDic["NormalEnemyAmount"]);
            levelData.bossAmount = int.Parse(sceneDic["BossEnemyAmount"]);
            levelData.isNormal = true;
            PlayerPrefsDataMgr.Instance.SaveData(levelData, "LevelPref");


        }



        NormalAmount = levelData.normalAmount;
        BossAmount = levelData.bossAmount;
        isNormal = levelData.isNormal;
        print("normal amount is : " + NormalAmount);
        print("Boss amount is: " + BossAmount);
        //在前面加Sprite可以把texture2d直接加载成sprite先加载成
        //texture2d再转换成sprite会抛null
        image = Resources.Load<Sprite>(levelMapLocation);
        if(image == null)
        {
            print("image is null");
        }
        //var sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.55f, 0.55f), 100.0f);
        //var sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.6f));
        transform.GetComponent<SpriteRenderer>().sprite = image;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NormalAmount == 0 && teleport == null && isNormal)
        {
            teleport = Instantiate(Resources.Load("Model/Teleport") as GameObject);
            teleport.layer = 8;
            teleport.AddComponent<Teleport>();

        }
        
    }

    public void changeToBossBackground()
    {
        sceneDic = GameConfigManager.Instance.GetSceneTypeById(levelId);
        levelMapLocation = sceneDic["BossLocation"];
        image = Resources.Load<Sprite>(levelMapLocation);
        transform.GetComponent<SpriteRenderer>().sprite = image;
        isNormal = false;
        LevelPlayerPref levelData = PlayerPrefsDataMgr.Instance.LoadData(typeof(LevelPlayerPref), "LevelPref") as LevelPlayerPref;
        levelData.isNormal = false;
        PlayerPrefsDataMgr.Instance.SaveData(levelData, "LevelPref");
        PlayerObject.Instance.transform.position = new Vector3(-9, -2.5f, 0);
        spawnBoss();
    }

    public void spawnBoss()
    {
        GameObject boss = GameObject.Find("Boss");
        GameObject obj = Instantiate(Resources.Load(sceneDic["BossModelLocation"]) as GameObject);
        obj.transform.SetParent(boss.transform);
        //obj.transform.position += new Vector3(0, 0.5f, 0);

        /*boss.AddComponent<Collider2D>();
        boss.AddComponent<Rigidbody2D>();*/
        obj.layer = 6;
    }
}
