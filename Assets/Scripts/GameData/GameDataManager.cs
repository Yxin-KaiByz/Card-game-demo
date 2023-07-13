using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance { get => instance; }
    public MusicData musicData;
    private GameDataManager() {
        
        
        //初始化游戏数据
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "Music") as MusicData;
        
        //如果没有数据要么是false要么是0

        if (!musicData.notFirst)
        {
            musicData.notFirst = true;
            musicData.isOpenBG = true;
            musicData.isOpenEffect = true;
            musicData.bgValue = 1;
            musicData.effectValue = 1;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

        
        }
    
    }

    //提供API方便外面改存储
    //开启或者关闭背景音乐
    public void oepnOrCloseBGMusic(bool isOpen)
    {
        musicData.isOpenBG = isOpen;
        //存储改变后的数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

    }

    //音效
    public void oepnOrCloseEffect(bool isOpen)
    {
        musicData.isOpenEffect = isOpen;
        //存储改变后的数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

    }

    public void changeBGValue(float value)
    {
        musicData.bgValue = value;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

    }

    public void changeEffectValue(float value)
    {
        musicData.effectValue = value;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");

    }





}
