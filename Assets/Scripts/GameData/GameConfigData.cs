using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏配置表类 每个对象对应一个txt配置表
public class GameConfigData
{
    //存储配置表中的所有数据，每个Dictionary存储一行的内容
    private List<Dictionary<string, string>> dataDic;

    public GameConfigData(string str)
    {
        //初始化list
        dataDic = new List<Dictionary<string, string>>();
        //换行切割
        string[] lines = str.Split('\n');
        //第一行是存储数据的类型 （trim删除字符串头尾空格）
        string[] title = lines[0].Trim().Split('\t');//tab
        //从第三行开始遍历数据，第二行数据是解释说明, 这个的主要作用是加键值对，
        //key是类型value是具体内容
        for(int i = 2; i < lines.Length; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] tempArr = lines[i].Trim().Split('\t');

            for(int j = 0; j < tempArr.Length; j++)
            {
                dic.Add(title[j], tempArr[j]);

            }

            dataDic.Add(dic);
        }
        //创建后会初始化整个数据为键值对储存在dataDic这个是一个表的所有内容根据给定的str

    }

    //返回整个list
    public List<Dictionary<string,string>> GetLines()
    {
        return dataDic;
    }

    //根据id找到dictionary
    public Dictionary<string,string> GetOneById(string id)
    {
        for(int i = 0; i < dataDic.Count; i++)
        {
            Dictionary<string,string> dic = dataDic[i];
            if (dic["Id"] == id)
            {
                return dic;
            }
        }
        return null;
    }
}
