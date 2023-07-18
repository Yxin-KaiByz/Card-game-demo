using DG.Tweening;
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

    public Animator animator;

    //组件相关
    SkinnedMeshRenderer _meshRender;

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        _meshRender = transform.GetComponentInChildren<SkinnedMeshRenderer>();
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
        enemyVec.y += 14;
        actionObj.transform.position = Camera.main.WorldToScreenPoint(enemyVec);

        SetRandomAction();

        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);
        UpdateHp();
        UpdateDefend();

        //test
        //OnSelect();
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

    //被攻击选中,显示红边
    public void OnSelect()
    {
        _meshRender.material.SetColor("_OtlColor", Color.red);
        transform.Find("cheche_bgremoved").GetComponent<SpriteRenderer>().color = Color.red;
    }

    //未选中
    public void OnUnSelect()
    {
        _meshRender.material.SetColor("_OtlColor", Color.black);
        transform.Find("cheche_bgremoved").GetComponent<SpriteRenderer>().color = Color.white;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //受伤
    public void Hit(int val)
    {
        //先扣护盾
        if(Defend >= val)
        {
            Defend -= val;

            //播放受伤(暂定1)
            animator.Play("cheche_attack", 0, 0);
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if(CurHp <= 0)
            {
                CurHp = 0;
                //播放死亡1
                animator.Play("cheche_attack", 0, 0);

                //移除
                EnemyManager.Instance.DeleteEnemy(this);
                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                //受伤
                animator.Play("cheche_attack", 0, 0);
            }
        }
        //刷新UI
        UpdateDefend();
        UpdateHp();
    }
    //隐藏怪物头上行动
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);

    }

    //执行敌人行动
    public IEnumerator DoAction()
    {
        HideAction();

        //播放对应动画（可以配置到excel，这里默认攻击）
        animator.Play("cheche_attack");
        //等待某一时间的后执行对应的行为（也可以放1excel）
        yield return new WaitForSeconds(0.5f);//不应该写死等0.5秒

        switch (actionType)
        {
            case E_ActionType.None:
                break;
            case E_ActionType.Attack:
                FightManager.Instance.GetPlayerHit(Attack);
                //抖动摄像机
                Camera.main.DOShakePosition(2, 1.5f, 5, 45);
                
                break;
            case E_ActionType.Defend:
                //加防御
                Defend += 1;
                UpdateDefend();
                //可以播放对应的特效
                break;
        }

        //等待动画播放完，这里时间长度也可以配置
        yield return new WaitForSeconds(1);

        //播放待机
        //animator.Play("idel");

    }
}
