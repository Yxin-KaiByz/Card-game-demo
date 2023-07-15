using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIMgr : MonoBehaviour
{
    public static UIMgr Instance;
    private Transform canvasTF;//画布变换组件
    private List<UIBase> uiList;//存储加载过的界面的集合

     private void Awake()
    {
        Instance = this;
        canvasTF = GameObject.Find("Canvas").transform;
        //初始化
        uiList = new List<UIBase>();
        
    }

    public UIBase ShowUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
       if(ui == null)
        {
            //instantiate as child of canvas
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTF) as GameObject;
            
            /*float width = canvasTF.GetComponent<RectTransform>().position.x;
            float height = canvasTF.GetComponent<RectTransform>().position.y;
            //obj.transform.position = new Vector2(obj.transform.InverseTransformPoint, 1110);
            RectTransform objRec = obj.GetComponent<RectTransform>();
            RectTransform rec = new RectTransform();
            
            obj.transform.SetParent(canvasTF);*/


            obj.name = uiName; 
            //添加需要的脚本
            ui = obj.AddComponent<T>();
            //添加到集合进行存储
            uiList.Add(ui);
        }
        else
        {
            
            ui.Show();
        }
       return ui;
    }
    //hide ui
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui !!= null)
        {
            ui.Hide();
        }
    }

    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if(ui != null)
        {
            uiList.Remove(ui);
            ui.Close();
        }
            
    }

    public void CloseAllUI()
    {
        foreach(UIBase ui in uiList)
        {
            ui.Close();
        }
        uiList.Clear();
    }

    //从集合中找到指定名字的界面脚本
    public UIBase Find(string uiName)
    {
        for(int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }
        }
        
        return null;
    }
}
