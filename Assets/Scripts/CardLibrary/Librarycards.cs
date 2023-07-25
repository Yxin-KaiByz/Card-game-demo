using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CardLibrary
{
    public class Librarycards : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Dictionary<string, string> data = null;
        private List<Dictionary<string, string>> IDdata;
        bool active = false;
        public void Init(Dictionary<string,string> data)
        {
            GameConfigManager.Instance.Init();
            this.data = data;
            IDdata = GameConfigManager.Instance.GetCardLibraryLines();
        }


        public void Update()
        {
            //active = IsActive();
            active = true;
            if (data != null)
            {
                loadImage(active);
            }
        }

        private bool IsActive()
        {
            bool status = false;
            for (int i = 0; i < IDdata.Count; i++)
            {
                if (IDdata[i]["Id"] == data["Id"])
                {
                    Type type = IDdata[i]["actived"].GetType();
                    // print(IDdata[i]["Id"] + "   It is "+ IDdata[i]["actived"] + type);
                    if (IDdata[i]["actived"] == "1")
                    {
                        status = true;
                        // print("status is " + status + "ID is " + IDdata[i]["Id"]);
                    }
                }
            }
            return status;
        }

        private void loadImage(bool active)
        {
            if (active)
            {
                // print("Actived Data" + data["Id"] + active );
;                //加载卡面上的数据
                transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
                transform.Find("bg").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
                transform.Find("bg/icon").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
                transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
                transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
                //先获得卡牌类型，然后通过这个类型再获得名字
                transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
                //设置bg背景image的外边框材质
                transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
            }
            else
            {
                // print("Non -Actived Data" + data["Id"] + active);
                transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
                transform.Find("bg").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
                transform.Find("bg/icon").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
                transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
                transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
                //先获得卡牌类型，然后通过这个类型再获得名字
                transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
                
            }
        }
    }
}