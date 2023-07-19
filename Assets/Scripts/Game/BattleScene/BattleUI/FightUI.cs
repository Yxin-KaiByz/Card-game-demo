using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//战斗界面
public class FightUI : UIBase
{
    private Text cardCountText;//卡牌数量
    private Text usedCardHeapText;//弃牌堆
    private Text pointText; //点数
    private Text hpText; //血量
    private Image hpImage;//血量图片
    private Text defenseText; //防御

    private List<CardItem> cardItemList = new List<CardItem>();//存储卡牌物体的集合体

    private void Awake()
    {
        cardCountText = this.transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardHeapText = this.transform.Find("noCard/icon/Text").GetComponent<Text>();
        pointText = this.transform.Find("mana/Text").GetComponent<Text>();
        hpText = this.transform.Find("hp/Text").GetComponent<Text>();
        hpImage = transform.Find("hp/fill").GetComponent<Image>();
        defenseText = transform.Find("hp/fangyu/Text").GetComponent<Text>();
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnButton);
    }

    //玩家会和结束，切换敌人回合
    private void onChangeTurnButton()
    {
        //只有玩家回合才能切换
        if(FightManager.Instance.fightUnit is PlayerTurn)
        {
            FightManager.Instance.ChnageType(E_FightType.Enemy);
        }
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

    //create card object
    public void CreateCardItem(int count)
    {
        
        if(count > FightCardManager.Instance.cardList.Count)
        {
            print("No more Cards");
            count = FightCardManager.Instance.cardList.Count;
        }
        //创建卡牌
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
            //得到抽取的卡牌id
            string cardId = FightCardManager.Instance.DrawCard();
            //根据id得到卡牌信息
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            //给卡牌UI加脚本
            //CardItem item = obj.AddComponent<CardItem>();
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            
            //存入cardItem
            item.Init(data);
            //把卡牌放进cardList
            //cardItemList.Add(item);
            cardItemList.Add(item);
        
        }
    }

    //update card position
    public void UpdateCardItemPos()
    {
        float offset = 800.0f / cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count / 2.0f * offset + offset * 0.5f, -700);
        for(int i = 0;i < cardItemList.Count;i++)
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x = startPos.x + offset;
        }
    }

    //删除卡牌物体

    public void RemoveCard(CardItem item) {

        //移除音效
        BattleAudio.Instance.changeEffect("SFX/Card/cardShove");
        item.enabled = false;//禁用卡牌

        //放入弃牌堆
        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);

        //更新使用后的卡牌数量
        usedCardHeapText.text = FightCardManager.Instance.usedCardList.Count.ToString();
        
        //从集合中删除
        cardItemList.Remove(item);

        //更新卡牌位置
        UpdateCardItemPos();

        //卡牌移到弃牌堆中的效果
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject, 1);
    
    
    }
    //删除所有卡
    public void RemoveAllCard()
    {
        for(int i = cardItemList.Count - 1; i >= 0; i--)
        {
            RemoveCard(cardItemList[i]);
        }
    }
}
