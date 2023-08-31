using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using Excel;
using System.Data;

public class ChooseAndAddData : MonoBehaviour
{

    [System.Serializable]
    public class ParticipantInfo
    {

        public int order;
        public float blinkTime;
        public float errorRadius;
        public float judgeTime;
        public float judgeTimeOther;
        public string changedParaName;
        public int cameraPosition;
    }
    [System.Serializable]
    public class ParticiList 
    {
        public List<ParticipantInfo> list = new List<ParticipantInfo>();
    }
    FollowEye gazeMouse;
    public bool chosed = false;
    // Start is called before the first frame update
    void Start()
    {
        gazeMouse = GameObject.FindObjectOfType<FollowEye>();
        FileInfo newFile = new FileInfo(Application.streamingAssetsPath + "/testResult.txt");
        

        if(newFile.Exists == false) 
        {
           newFile = new FileInfo(Application.streamingAssetsPath + "/testResult.txt");
        }
        


        //数据操作
        //Debug.Log(Application.dataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChooseAddData() 
    {  // not checked// write file
        if (chosed) return;//only once a time
        chosed = true;
        //FileInfo newFile = new FileInfo(Application.streamingAssetsPath + "/testResult.txt");
        ParticipantInfo pI = new ParticipantInfo();
        pI.order = 0;
        pI.blinkTime = FollowEye.blinkTime;
        pI.errorRadius= gazeMouse.errorLen;
        pI.judgeTime = FollowEye.FixedChooseTime;
        pI.judgeTimeOther = FollowEye.EmptyLookTime;
        pI.changedParaName = gameObject.scene.name;
        pI.cameraPosition = MoveRotationCamera.CameraNum;
        string toWrite =  JsonUtility.ToJson(pI);
        File.AppendAllText(Application.streamingAssetsPath + "/testResult.txt", toWrite);


        //数据操作
        
        Application.Quit();
       
           
    }
}

