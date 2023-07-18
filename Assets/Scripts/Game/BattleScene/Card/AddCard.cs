using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//加牌
public class AddCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if(TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);//抽卡数量

            if (FightCardManager.Instance.HasCard() == true)
            {
                //是否有卡抽
                UIMgr.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);
                UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                UIMgr.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));

                playEffect(pos);
            }
            else
            {
                base.OnEndDrag(eventData);
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }
        
    }
}
