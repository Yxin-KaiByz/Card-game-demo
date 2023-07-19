using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//lose
public class FightLose : FightUnit
{

    public override void Init()
    {
        Debug.Log("Lost");
        FightManager.Instance.StopAllCoroutines();
        //œ‘ æ ß∞‹ΩÁ√Ê
    
    }

    public override void OnUpdate()
    {
        
    }

}
