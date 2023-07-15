using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {
        
        Debug.Log("Init complete");

        //显示战斗界面
        UIMgr.Instance.ShowUI<FightUI>("FightUI");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }


}
