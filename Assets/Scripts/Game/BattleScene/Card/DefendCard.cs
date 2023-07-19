using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//defend card add shield
public class DefendCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if(TryUse() == true)
        {
            //使用效果
            int val = int.Parse(data["Arg0"]);

            //使用播放后的声音(每张卡使用的声音可能不一样)
            BattleAudio.Instance.changeEffect("Effect/healspell");

            //增加防御力
            FightManager.Instance.DefenseCount += val;
            //刷新文本
            UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

            Vector3 pos = Camera.main.transform.position;
            pos.y = -70;
            pos.x += 1300 ;
            playEffect(pos);

        }
        else
        {
            base.OnEndDrag(eventData);
        }

       
    }
}
