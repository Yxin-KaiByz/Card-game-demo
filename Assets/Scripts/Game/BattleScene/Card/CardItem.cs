using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Dictionary<string, string> data;
    public string drawEffect;
    public string hoverEffect;
    public string placeEffect;


    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
        drawEffect = "Cards/draw";
        hoverEffect = "Cards/cardShove";
        placeEffect = "Cards/cardPlace";
        

    }

    private int index;

    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        //慢慢放大到1.5每次增加0.25
        transform.DOScale(1.5f, 0.25f);
        //这两步是改变卡牌的渲染顺序，因为放大要遮挡关系
        //所以把他设置成最后渲染
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        //通过边框给卡牌上色
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        //回到1
        transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    private void Start()
    {
        //加载卡面上的数据
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"],data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        //先获得卡牌类型，然后通过这个类型再获得名字
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

        //设置bg背景image的外边框材质
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
    }
    Vector2 initPos;//拖拽开始记录卡牌位置

    //end drag
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }

    //dragging card
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out pos
              ))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }    



    }
    //drag card
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //播放声音

        BattleAudio.Instance.changeEffect(drawEffect);


        
    }

    //try to use the card
    public virtual bool TryUse()
    {
        //所需费用
        int cost = int.Parse(data["Expend"]);

        if(cost > FightManager.Instance.CurPointCount)
        {
            //费用不足
            BattleAudio.Instance.changeEffect("SFX/Effect/Loss");

            //提示
            UIMgr.Instance.ShowTip("No Enough Point", Color.red);

            return false;

        }
        else
        {
            FightManager.Instance.CurPointCount -= cost;
            //刷新费用文本
            UIMgr.Instance.GetUI<FightUI>("FightUI").UpdatePoint();

            //使用的卡牌删除
            UIMgr.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);

            return true;
        }
    }

    //创建卡牌使用后的特效
    public void playEffect(Vector3 pos)
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
       
        effectObj.transform.position = pos;
        //effectObj.transform.SetParent(GameObject.Find("Canvas").transform);
        
        Destroy(effectObj, 2);
    }

   
}
