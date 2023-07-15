using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//战斗界面
public class FightUI : UIBase
{
    private Text cardCountText;//卡牌数量
    private Text usedCardHeapText;//弃牌堆
    private Text pointText; //点数
    private Text hpText; //血量
    private Image hpImage;//血量图片
    private Text defenseText; //防御

    private void Awake()
    {
        cardCountText = this.transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardHeapText = this.transform.Find("noCard/icon/Text").GetComponent<Text>();
        pointText = this.transform.Find("mana/Text").GetComponent<Text>();
        hpText = this.transform.Find("hp/Text").GetComponent<Text>();
        hpImage = transform.Find("hp/fill").GetComponent<Image>();
        defenseText = transform.Find("hp/fangyu/Text").GetComponent<Text>();
    }

    public void Start()
    {
        UpdateHP();
        UpdatePoint();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardHeapCount();

    }

    //更新血量显示
    public void UpdateHP()
    {
        hpText.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImage.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;
    }

    //更新点数
    public void UpdatePoint()
    {
        pointText.text = FightManager.Instance.CurPointCount + "/" + FightManager.Instance.MaxPointCount;
    }

    //防御更新
    public void UpdateDefense()
    {
        //print("defense is: " + FightManager.Instance.DefenseCount.ToString());
        defenseText.text = FightManager.Instance.DefenseCount.ToString();
    }

    //更新卡堆卡牌数量
    public void UpdateCardCount()
    {
        cardCountText.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    //更新弃牌堆数量
    public void UpdateUsedCardHeapCount()
    {
        usedCardHeapText.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }
}
