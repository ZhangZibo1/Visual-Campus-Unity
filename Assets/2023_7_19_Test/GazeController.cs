using UnityEngine;
using Tobii.Gaming;
using System.Collections.Generic;
using System.IO;
using System;
using OfficeOpenXml;
using System.Data;
using Excel;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using UnityEngine.UI;

public class GazeController : MonoBehaviour
{
    //Gaze point in screen coordination
    private GazePoint _lastHandledPoint = GazePoint.Invalid;
   //Gaze information in the procedure
    private float _pauseTimer;
    private float X = 0;
    private float Y = 0;
    public float timeStamp;
    private string path;
    public Vector2 gazeScreenPosition;
    FileInfo _excelName;
    ExcelWorksheet worksheet;
    ExcelPackage package;
    string _sheetName = "GazeData";
    int index;

    void Start()
    {

        //path = @"C:/GazeData/GazeData.txt";
        //// This text is added only once to the file.
        //if (!File.Exists(path))
        //{
        //    // Create a file to write to.
        //    string createText = "This is the gaze data:" + Environment.NewLine;
        //    File.WriteAllText(path, createText);
        //}
        index = 2;
        string _filePath = @"C:/GazeData/gazedata.xlsx";
        _excelName = new FileInfo(_filePath);
        if (_excelName.Exists)
        {
            //删除旧文件，并创建一个新的 excel 文件。
            _excelName.Delete();
            _excelName = new FileInfo(_filePath);
        }
        package = new ExcelPackage(_excelName);      
        //在 excel 空文件添加新 sheet，并设置名称。
        worksheet = package.Workbook.Worksheets.Add(_sheetName);
        //添加列名
        worksheet.Cells[1, 1].Value = "TimeStamp";
        worksheet.Cells[1, 2].Value = "X";
        worksheet.Cells[1, 3].Value = "Y";       
    }
    void Update()
    {
        gazeScreenPosition = Input.mousePosition;///之后删除

        //Check time up
        if (_pauseTimer > 0)
        {
            _pauseTimer -= Time.deltaTime;
            return;
        }
        //Gaze operation
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        if (gazePoint.IsValid)
        {
            //Calculate fixation points
            IEnumerable<GazePoint> pointsSinceLastHandled = TobiiAPI.GetGazePointsSince(_lastHandledPoint);
            int count = 0;
            using (IEnumerator<GazePoint> enumerator = pointsSinceLastHandled.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    count++;
            }
            float averX = 0;
            float averY = 0;
            int index = 0;
            foreach (var point in pointsSinceLastHandled)
            {

                if (index < count)
                {
                    averX += point.Screen.x;
                    averY += point.Screen.y;
                    index++;
                }
                else
                    break;
            }
            averX /= count;
            averY /= count;          
            X = averX;
            Y = averY;
            gazeScreenPosition = new Vector2(X, Y);
       
        }
        //添加数据
        worksheet.Cells[index, 1].Value = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        worksheet.Cells[index, 2].Value = X;
        worksheet.Cells[index, 3].Value = Y;
        package.Save();
        index++;
     
        // This text is always added, making the file longer over time
        // if it is not deleted.
        //string appendText = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":"+ " " + X + " " + Y +" " + timeStamp + Environment.NewLine;
        //File.AppendAllText(path, appendText);

    }

}

