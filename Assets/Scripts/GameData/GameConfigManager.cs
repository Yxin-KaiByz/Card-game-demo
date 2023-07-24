using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//整个游戏配置表的管理器
public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    private GameConfigData cardData;//card
    private GameConfigData enemyData;//enemies
    private GameConfigData levelData;//level
    private GameConfigData cardTypeData;//card Type
    private GameConfigData sceneData; //background scene
    private GameConfigData cardLibrary; //card library

    //用来load txt文件通过.txt的方法可以转换成string
    private TextAsset textAsset;

    //初始化配置文件（txt文件存储到内存中）
    public void Init()
    {
        textAsset = Resources.Load<TextAsset>("Data/card");
        //把路径给GameConfigData在instantiate的时候就会完成键值对的存储
        cardData = new GameConfigData(textAsset.text);
        
        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);
        
        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/scene");
        sceneData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardLibrary");
        cardLibrary = new GameConfigData(textAsset.text);
    }

    //获取卡牌的存储键值对的list
    public List<Dictionary<string,string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }

    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }

    public List<Dictionary<string, string>> GetSceneLines()
    {
        return sceneData.GetLines();
    }

    public List<Dictionary<string, string>> GetCardTypeLines()
    {
        return cardTypeData.GetLines();
    }

    public List<Dictionary<string, string>> GetCardLibraryLines()
    {
        return cardLibrary.GetLines();
    }

    //通过id获取卡牌
    public Dictionary<string, string> GetCardById(string id)
    {
        return cardData.GetOneById(id);
    }

    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }

    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    public Dictionary<string, string> GetCardTypeById(string id)
    {
        return cardTypeData.GetOneById(id);
    }

    public Dictionary<string, string> GetSceneTypeById(string id)
    {
        return sceneData.GetOneById(id);
    }

}
