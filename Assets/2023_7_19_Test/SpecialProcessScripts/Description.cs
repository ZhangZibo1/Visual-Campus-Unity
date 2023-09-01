using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Description : MonoBehaviour
{
    public GazeController cursor;
    public static bool eyeOnPanel = false;
    public GameObject descritption;
    public FollowEye gazeMouse;

    float moveByMouseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        eyeOnPanel = false;
        gazeMouse = GameObject.FindObjectOfType<FollowEye>();
        moveByMouseSpeed = ToPersonCamera.MainCamera.GetComponent<MoveRotationCamera>().moveByMouseSpeed;
        cursor = GameObject.Find("GazeController").GetComponent<GazeController>();
    }
    

    // Update is called once per frame
    void Update()
    {   
        

        if (FollowEye.chooseBuild)
        {
            Vector3 tarPosition = FollowEye.tarBuilding.transform.position + FollowEye.tarBuilding.transform.GetComponent<BoxCollider>().center;
            tarPosition = ToPersonCamera.activeCamera.WorldToScreenPoint(tarPosition);
            if (FollowEye.closeToTarBuilding)
            {
                tarPosition = gazeMouse.transform.position;
            }

            if (Description.eyeOnPanel == true)
            {
                Vector3 temp = gazeMouse.transform.position;
                temp.x = tarPosition.x;
                temp.y = tarPosition.y;
                gazeMouse.transform.position = temp;
            }
            ToPersonCamera.MainCamera.GetComponent<MoveRotationCamera>().moveByMouseSpeed = 0.1f;
            //Debug.Log("activePanel");
            descritption.gameObject.SetActive(true);
            descritption.gameObject.transform.position = tarPosition;
            descritption.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = FollowEye.tarBuilding.name;
            descritption.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text ="current people: "+ FollowEye.tarBuilding.GetComponent<DescriptText>().descriptionTexts[0];

            float x = Mathf.Abs(cursor.gazeScreenPosition.x - tarPosition.x);
            float y = Mathf.Abs(cursor.gazeScreenPosition.y - tarPosition.y);
            //Debug.Log(x + " " + y + " " + descritption.GetComponent<RectTransform>().sizeDelta.x / 2 + " " +  descritption.GetComponent<RectTransform>().sizeDelta.y / 2);

            if ( x < descritption.GetComponent<RectTransform>().sizeDelta.x/1920*Screen.width/2 + FollowEye.errorLen &&
                y < descritption.GetComponent<RectTransform>().sizeDelta.y/1920*Screen.height/2+ FollowEye.errorLen)
            {
                //Debug.Log("focus on pannel");
                //Debug.Log("hit Building" + hit.transform.name);
                eyeOnPanel = true;
                FollowEye.eyeOnNPC = false;
            }
            else
            {
                eyeOnPanel = false;
                //Debug.Log("eyeOnpanel"+eyeOnPanel);
            }

           

        }
        

        //Debug.Log("Fixed something"+FollowEye.FixedSomething);
        if (!eyeOnPanel || !FollowEye.FixedSomething)
        {
            //eyeOnPanel = false;
            ToPersonCamera.MainCamera.GetComponent<MoveRotationCamera>().moveByMouseSpeed = moveByMouseSpeed;
            descritption.gameObject.SetActive(false);
        }
        
    }
}
