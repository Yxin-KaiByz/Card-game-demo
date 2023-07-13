using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicData 
{
    //background music and effect is open or not
    public bool isOpenBG;
    public bool isOpenEffect;
    //specific volume
    public float bgValue;
    public float effectValue;

    //是否第一次加载数据
    public bool notFirst;
}
