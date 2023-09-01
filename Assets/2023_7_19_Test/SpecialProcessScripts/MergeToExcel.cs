using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using Excel;
using System.Data;

public class MergeToExcel : MonoBehaviour
{
    public string[] txtPath;
    [System.Serializable]
    public class ParticipantInfo
    {

        public int order;
        public float blinkTime;
        public float errorRadius;
        public float judgeTime;
        public float judgeTimeOther;
        public int cameraPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        {
            FileInfo newFile = new FileInfo(Application.dataPath + "/testResult.xlsx");
            //如果文件存在删除重建

            if (newFile.Exists)
            {
                //删除旧文件，并创建一个新的 excel 文件。
                newFile.Delete();
                newFile = new FileInfo(Application.dataPath + "/testResult.xlsx");
            }
            //数据操作
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                //初次创建增加数据操作（重点在于这条操作语句不同）
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("testResult");

                //添加对应列名
                worksheet.Cells[1, 1].Value = "Order Of Participants";
                worksheet.Cells[1, 2].Value = "Blink Time";
                worksheet.Cells[1, 3].Value = "Error Radius";
                worksheet.Cells[1, 4].Value = "Judge Time (building,UI, character)";
                worksheet.Cells[1, 5].Value = "Judge Time(other)";
                //worksheet.Cells[1, 6].Value = "Where Choose Preference";
                worksheet.Cells[1, 6].Value = "Camera Position";
                //保存
                package.Save();
            }
        }
        int count = 1;
           for(int i =0; i < txtPath.Length; i++)
        {
            string filePath = txtPath[i] + "/testResult.txt";

            string[] lines = File.ReadAllLines(filePath, System.Text.Encoding.Default);
           
            for(int j = 0; j < lines.Length; j++) 
            {
                Debug.Log(lines[j]);
                ParticipantInfo pi =  JsonUtility.FromJson<ParticipantInfo>(lines[j]);
                Debug.Log(pi);
                count++;
                FileInfo newFile = new FileInfo(Application.dataPath + "/testResult.xlsx");
                //如果文件存在删除重建


                //数据操作
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    ExcelWorksheet worksheet;
                    //初次创建增加数据操作（重点在于这条操作语句不同）
                   
                    worksheet = package.Workbook.Worksheets["testResult"];


                    //添加对应列名
                    worksheet.Cells[count, 1].Value = count - 1;
                    worksheet.Cells[count, 2].Value = pi.blinkTime;
                    worksheet.Cells[count, 3].Value = pi.errorRadius;
                    worksheet.Cells[count, 4].Value = pi.judgeTime;
                    worksheet.Cells[count, 5].Value = pi.judgeTimeOther;
                    
                    worksheet.Cells[count, 6].Value = pi.cameraPosition;
                    //保存
                    package.Save();
                
                }

            }


        }
            
           
                
                
       
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }

    
}
