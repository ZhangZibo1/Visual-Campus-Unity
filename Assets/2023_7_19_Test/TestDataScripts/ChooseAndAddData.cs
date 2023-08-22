using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using Excel;
using System.Data;

public class ChooseAndAddData : MonoBehaviour
{

    FollowEye gazeMouse;
    public bool chosed = false;
    // Start is called before the first frame update
    void Start()
    {
        gazeMouse = GameObject.FindObjectOfType<FollowEye>();
        FileInfo newFile = new FileInfo(Application.dataPath + "/testResult.xlsx");
        //如果文件存在删除重建
        

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
            worksheet.Cells[1, 6].Value = "Where Choose Preference";
            worksheet.Cells[1, 7].Value = "Camera Position";
            //保存
            package.Save();
        }
        Debug.Log(Application.dataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChooseAddData() 
    {  // not checked// write file
        if (chosed) return;//only once a time
        chosed = true;
        FileInfo newFile = new FileInfo(Application.dataPath + "/testResult.xlsx");
        //数据操作
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            //增加数据操作（重点在于这条操作语句与初次创建添加数据不同）
            ExcelWorksheet worksheet = package.Workbook.Worksheets["testResult"];

            //添加第二行数据
            for(int i = 1;i < 100000000 ; i++) 
            {
                if(worksheet.Cells[i, 1].Value == null) 
                {
                    worksheet.Cells[i, 1].Value = i - 1;
                    worksheet.Cells[i, 2].Value = FollowEye.blinkTime;
                    worksheet.Cells[i, 3].Value = gazeMouse.errorLen;
                    worksheet.Cells[i, 4].Value = FollowEye.FixedChooseTime;
                    worksheet.Cells[i, 5].Value = FollowEye.EmptyLookTime;
                    worksheet.Cells[i, 6].Value = gameObject.scene.name;
                    worksheet.Cells[i, 7].Value = MoveRotationCamera.CameraNum;
                    break;
                }
            }
           

            //保存
            package.Save();
        }
    }
}
