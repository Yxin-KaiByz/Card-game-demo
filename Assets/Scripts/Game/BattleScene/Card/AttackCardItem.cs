using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//attack card
public class AttackCardItem : CardItem, IPointerDownHandler
{

    public override void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public override void OnDrag(PointerEventData eventData)
    {
        
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        
    }



    //按下
    public void OnPointerDown(PointerEventData eventData)
    {
        //播放声音
        BattleAudio.Instance.changeEffect("Cards/draw");

        //显示曲线界面
        UIMgr.Instance.ShowUI<LineUI>("LineUI");


        //设置开始点位置
        UIMgr.Instance.GetUI<LineUI>("LineUI").SetStarPos(transform.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 100));

        //隐藏鼠标
        Cursor.visible = false;
        //关闭1所有协同程序
        StopAllCoroutines();
        //启动鼠操作协同程序
        StartCoroutine(OnMouseDownRight(eventData));
    }

    IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        while (true)
        {
            //如果再次按下鼠标右键跳出循环1
            if (Input.GetMouseButton(1))
            {
                break;
            }

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    transform.parent.GetComponent<RectTransform>(),
                    pData.position,
                    pData.pressEventCamera,
                    out pos
                
                ))
            {
                //设置箭头位置
                UIMgr.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);
                //进行射线检测是否碰到怪物
                CheckRayToEnemy();
            }
            yield return null;
        }

        //跳出循环后 显示鼠标
        Cursor.visible = true;
        //关闭曲线
        UIMgr.Instance.CloseUI("LineUI");
    }

    Enemy hitEnemy; //射线检测到敌人脚本
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 v = ray.GetPoint(0);
        //print(v.ToString());

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            //print("选中啦啦啦啦啦啦啦啦啦啦啦啦啦啦啦");
            hitEnemy = hit.transform.GetComponent<Enemy>();

            hitEnemy.OnSelect();//选中

            //如果按下左键使用攻击卡
            if(Input.GetMouseButtonDown(0)) {
                //关闭所有协同程序；
                StopAllCoroutines();

                //鼠标显示
                Cursor.visible = true;
                UIMgr.Instance.CloseUI("LineUI");
                if (TryUse() == true)
                {
                    //播放特效
                    playEffect(hitEnemy.transform.position);

                    //打击特效
                    BattleAudio.Instance.changeEffect("Effect/sword");
                    //敌人受伤
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);
                
                }
                //敌人未选中
                hitEnemy.OnUnSelect();
                //设置敌人脚本为null
                hitEnemy = null;
            }
            

        }
        else
        {
            //未射到怪物
            if (hitEnemy != null)
            {
               hitEnemy.OnUnSelect();
                hitEnemy = null;
            }
        }

    }
}
