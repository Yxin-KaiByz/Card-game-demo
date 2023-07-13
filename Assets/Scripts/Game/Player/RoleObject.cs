using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleObject : MonoBehaviour
{
    //moving direction
    protected Vector2 moveDir = Vector2.zero;
    //角色对象基类，之后角色都可以继承它
    protected void changeAction(E_Action_Type type)
    {
        switch (type)
        {
            case E_Action_Type.Idle:
                roleAnimator.SetBool("isMoving", false);
                break;
            case E_Action_Type.Move:
                roleAnimator.SetBool("isMoving",true);
                break;
            case E_Action_Type.Atk1:
                break;
            case E_Action_Type.Atk2:
                break;
            case E_Action_Type.Ult:
                break;
            case E_Action_Type.SkillAtck:
                break;
            case E_Action_Type.Defend:
                break;
            case E_Action_Type.Hitted:
                break;
            default:
                break;
        }
    }
    public float moveSpeed = 5;


    protected SpriteRenderer roleSprite;
    protected Animator roleAnimator;

    protected virtual void Awake()
    {
        roleSprite = this.GetComponentInChildren<SpriteRenderer>();
        roleAnimator = this.GetComponentInChildren<Animator>(); 
    }

    protected virtual void Update()
    {
        //move logic
        this.transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        //turnAround logic,ignore 0 condition
        if(moveDir.x > 0)
        {
            roleSprite.flipX = false;
        }else if(moveDir.x < 0)
        {
            roleSprite.flipX = true;
        }
        if(moveDir == Vector2.zero)
        {
            changeAction(E_Action_Type.Idle);
        }
        else
        {
            changeAction(E_Action_Type.Move);
        }
    }
}

public enum E_Action_Type
{
    Idle,
    Move,
    Atk1,
    Atk2,
    Ult,
    SkillAtck,
    Defend,
    Hitted
}
