using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum E_ActionType
{
    None,
    Defend,
    Attack
}

public class Enemy : MonoBehaviour
{
    //enemy data
    private Dictionary<string, string> data;
    public E_ActionType actionType;
    public GameObject hpItemObj;
    public GameObject actionObj;

    //怪物行动的ui
    public Transform attackTf;
    public Transform defendTf;
    //血量防御ui
    public Text defendText;
    public Text hpText;
    public Image hpImage;

    //数值相关,这个是数值上面是Text在UI里
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    // Start is called before the first frame update
    void Start()
    {
        //设置初始行动为None
        actionType = E_ActionType.None;
        //创建怪物的两个UI
        hpItemObj = UIMgr.Instance.CreateHPItem();
        actionObj = UIMgr.Instance.CreateActionIcon();

        //得到attack defense的transform
        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");

        //得到数值text
        defendText = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpText = hpItemObj.transform.Find("hpText").GetComponent<Text>();
        hpImage = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //设置血条 行动力位置
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Bottom").position);
        //actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Head").position);
        Vector3 enemyVec = transform.Find("Head").position;
        enemyVec.z = 0;
        actionObj.transform.position = Camera.main.WorldToScreenPoint(enemyVec);

        SetRandomAction();

        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);
        UpdateHp();
        UpdateDefend();
    }



    //随机一个行动
    public void SetRandomAction()
    {
        int rand = Random.Range(1, 3);//三个action中的一个
        actionType = (E_ActionType)rand;
        switch(actionType)
        {
            case E_ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;
            case E_ActionType.None:
                break;
            case E_ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
        }
    }

    //更新血量
    public void UpdateHp()
    {
        hpText.text = CurHp + "/" + MaxHp;
        hpImage.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //更新防御
    public void UpdateDefend()
    {
        defendText.text = Defend.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
