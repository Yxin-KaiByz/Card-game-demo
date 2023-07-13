using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : RoleObject
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        //父类相关的Awake逻辑一定概要保留
        base.Awake();
        //开启输入控制
        InputMgr.GetInstance().StartOrEndCheck(true);
        //获取输入权限
        GetController();
    }

    protected override void Update()
    {
        //一定要保持这个base.Update的存在 因为 移动逻辑 是写在父类中的
        //除非之后你要重写 才不需要它
        base.Update();
    }

    /// <summary>
    /// 给予控制权
    /// </summary>
    public void GetController()
    {
        //事件中心 有加 就有减 一定不要往里面传 那么大表达式 一定是在下方去声明一个函数
        EventCenter.GetInstance().AddEventListener<float>("Horizontal", CheckX);
        EventCenter.GetInstance().AddEventListener<float>("Vertical", CheckY);
        //监听按键按下内容
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
    }

    /// <summary>
    /// 剥夺控制权
    /// </summary>
    public void RemoveController()
    {
        //事件中心 有加 就有减 一定不要往里面传 那么大表达式 一定是在下方去声明一个函数
        EventCenter.GetInstance().RemoveEventListener<float>("Horizontal", CheckX);
        EventCenter.GetInstance().RemoveEventListener<float>("Vertical", CheckY);
        //监听按键按下内容
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
    }

    private void CheckX(float x)
    {
        //x 就会是 -1 0 1三个值的数 
        // 按 A 为-1  不按为0  按D为1
        //获取横向输入方向 
        moveDir.x = x;
    }

    private void CheckY(float y)
    {
        //x 就会是 -1 0 1三个值的数 
        // 按 S 为-1  不按为0  按W为1
        //获取纵向输入方向
        moveDir.y = y;
    }

    /// <summary>
    /// 检测玩家 除移动意外的 输入内容
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyDown(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.J:
                print("J键");
                break;
            case KeyCode.K:
                print("K键");
                break;
            case KeyCode.L:
                print("L键");
                break;
            case KeyCode.Space:
                print("Space键");
                break;
        }
    }


    private void OnDestroy()
    {
        //事件 有加就有减 移除时一定要注销事件
        RemoveController();
    }
}
