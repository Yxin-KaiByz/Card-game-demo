using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CardLibrary
{
    public class Librarycards : MonoBehaviour
    {
        public Dictionary<string, string> data = null;

        public void Init(Dictionary<string,string> data)
        {
            this.data = data;

        }


        public void Update()
        {
            if (data != null)
            {
                //加载卡面上的数据
                transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
                transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
                transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
                transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
                transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
                //先获得卡牌类型，然后通过这个类型再获得名字
                transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
                //设置bg背景image的外边框材质
                transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
            }
        }
    }
}