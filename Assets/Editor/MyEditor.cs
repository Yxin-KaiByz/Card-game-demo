using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel; //读取Excel,要用到excel相关的plugin
using System.Data;
using System.Drawing.Printing;

//编辑器脚本
public class MyEditor
{
    //给unity头顶添加功能
    [MenuItem("MyTool/excel to txt")]
    public static void ExportExcelToTxt()
    {
        //_Excel文件夹路径
        string assetPath = Application.dataPath + "/_Excel";


        //获得文件夹中excel文件
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");

        for(int i = 0; i < files.Length; i++)
        {
            //replace \ to /
            files[i] = files[i].Replace('\\', '/');

            //通过文件流读取文件
            using (FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            {
                //文件流转成excel对象
                var excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);

                //获取excel数据
                DataSet dataSet = excelDataReader.AsDataSet();

                //读取excel第一张表
                DataTable table = dataSet.Tables[0];

                //将表中内容读取后存储到对应的txt文件
                readTableToTxt(files[i], table);



            }
        }

        //刷新编译器
        AssetDatabase.Refresh();




    }

    private static void readTableToTxt(string filePath, DataTable table)
    {
        //获取文件名(不要文件后缀生成与之名字相同的txt文件) 没有后缀文件类型名txt xml这种
        string fileName = Path.GetFileNameWithoutExtension(filePath);

        //txt文件存储路径
        string path = Application.dataPath + "/Resources/Data/" + fileName + ".txt";

        //判断reources/Data文件夹中是否已经存在对应txt，如果是则删除
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        //文件流创建txt文件
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            //文件流转写入流，方便写入字符串
            using (StreamWriter sw = new StreamWriter(fs))
            {
                //遍历table
                for(int row = 0; row < table.Rows.Count; row++)
                {
                    DataRow dataRow = table.Rows[row];
                    string str = "";
                    //遍历列
                    for(int col = 0; col < table.Columns.Count; col++)
                    {
                        string val = dataRow[col].ToString();

                        str = str + val + "\t"; //每一项tab分割
                    }

                    //写入
                    sw.Write(str);

                    //如果不是最后一行 换行
                    if(row != table.Rows.Count - 1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    
    }


}
