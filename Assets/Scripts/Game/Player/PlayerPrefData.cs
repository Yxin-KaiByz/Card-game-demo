using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefData
{
    public static PlayerPrefData instance;
    public int MaxHp;
    public int CurHp;


    private PlayerPrefData() {

        MaxHp = 10;
        CurHp = 10;


    }

    public static PlayerPrefData Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerPrefData();
            }
            return instance;
        }
        
    }
    


}
