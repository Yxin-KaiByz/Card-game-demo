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

    //�����ж���ui
    public Transform attackTf;
    public Transform defendTf;
    //Ѫ������ui
    public Text defendText;
    public Text hpText;
    public Image hpImage;

    //��ֵ���,�������ֵ������Text��UI��
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    public Animator animator;

    //������
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
        //���ó�ʼ�ж�ΪNone
        actionType = E_ActionType.None;
        //�������������UI
        hpItemObj = UIMgr.Instance.CreateHPItem();
        actionObj = UIMgr.Instance.CreateActionIcon();

        //�õ�attack defense��transform
        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");

        //�õ���ֵtext
        defendText = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpText = hpItemObj.transform.Find("hpText").GetComponent<Text>();
        hpImage = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //����Ѫ�� �ж���λ��
        // hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Bottom").position);
        hpItemObj.transform.position = transform.Find("Bottom").position;
        //actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("Head").position);
        Vector3 enemyVec = transform.Find("Head").position;
        enemyVec.z = 0;
        enemyVec.y += 20;
        // actionObj.transform.position = Camera.main.WorldToScreenPoint(enemyVec);
        actionObj.transform.position = enemyVec;

        SetRandomAction();

        //��ʼ����ֵ
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);
        UpdateHp();
        UpdateDefend();

        //test
        //OnSelect();
    }



    //���һ���ж�
    public void SetRandomAction()
    {
        int rand = Random.Range(1, 3);//����action�е�һ��
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

    //����Ѫ��
    public void UpdateHp()
    {
        hpText.text = CurHp + "/" + MaxHp;
        hpImage.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //���·���
    public void UpdateDefend()
    {
        defendText.text = Defend.ToString();
    }

    //������ѡ��,��ʾ���
    public void OnSelect()
    {
        _meshRender.material.SetColor("_OtlColor", Color.red);
        //transform.Find("cheche_bgremoved").GetComponent<SpriteRenderer>().color = Color.red;
        transform.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    //δѡ��
    public void OnUnSelect()
    {
        _meshRender.material.SetColor("_OtlColor", Color.black);
        //transform.Find("cheche_bgremoved").GetComponent<SpriteRenderer>().color = Color.white;
        transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }





    // Update is called once per frame
    void Update()
    {
        
    }

    //����
    public void Hit(int val)
    {
        //�ȿۻ���
        if(Defend >= val)
        {
            Defend -= val;

            //��������(�ݶ�1)
            print(animator.name.Split("_")[0] + "_attack");
            animator.Play(animator.name.Split("_")[0] + "_hit", 0, 0);
            //animator.SetBool("Attack", true);
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if(CurHp <= 0)
            {
                CurHp = 0;
                //��������1
                //animator.Play("cheche_attack", 0, 0);
                print(animator.name.Split("_")[0] + "_die");
                animator.Play(animator.name.Split("_")[0] + "_die", 0, 0);

                //�Ƴ�
                EnemyManager.Instance.DeleteEnemy(this);
                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                //����
                print(animator.name.Split("_")[0]);
                animator.Play(animator.name.Split("_")[0] + "_hit", 0, 0);
                //animator.SetBool("Attack", true);

            }
        }
        //ˢ��UI
        UpdateDefend();
        UpdateHp();
        
       
    }
    //���ع���ͷ���ж�
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);

    }

    //ִ�е����ж�
    public IEnumerator DoAction()
    {
        HideAction();

        //���Ŷ�Ӧ�������������õ�excel������Ĭ�Ϲ�����
        //animator.Play("cheche_attack");
        animator.Play(animator.name.Split("_")[0] + "_attack", 0, 0);
        //�ȴ�ĳһʱ��ĺ�ִ�ж�Ӧ����Ϊ��Ҳ���Է�1excel��
        yield return new WaitForSeconds(0.5f);//��Ӧ��д����0.5��

        switch (actionType)
        {
            case E_ActionType.None:
                break;
            case E_ActionType.Attack:
                FightManager.Instance.GetPlayerHit(Attack);
                //���������
                Camera.main.DOShakePosition(1, 3, 5, 45);

                break;
            case E_ActionType.Defend:
                //�ӷ���
                Defend += 1;
                UpdateDefend();
                //���Բ��Ŷ�Ӧ����Ч
                break;
        }

        //�ȴ����������꣬����ʱ�䳤��Ҳ��������
        yield return new WaitForSeconds(1);

        //���Ŵ���
        //animator.Play("idel");

    }
}
