using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InNPCView : MonoBehaviour
{
       
    public GazeController cursor;
    public  bool eyeOnPanel = false;
    public GameObject descritption;
    public FollowEye gazeMouse;
    public bool seeNPC;
    float moveByMouseSpeed;
    GazeController gazeContro;

    public static RaycastHit hit;
    public static Transform hitParent;
    public static bool eyeOnSamllBuil;

    public float countTime = 0;
    bool allowChange = true;
    // Start is called before the first frame update
    void Start()
    {
        gazeMouse = GameObject.FindObjectOfType<FollowEye>();
        moveByMouseSpeed = ToPersonCamera.MainCamera.GetComponent<MoveRotationCamera>().moveByMouseSpeed;
        gazeContro = GameObject.Find("GazeController").GetComponent<GazeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ToPersonCamera.inNPCView) {  descritption.SetActive(false); return; }

        if (countTime >= 0 && !allowChange)
        {
            countTime -= Time.deltaTime;
        }
       if(countTime < 0 && !allowChange)
        {
            allowChange = true ;
        }
        
        
        ShowPanelOnSmallBuild();
        ShowPanelOnNPC();


        //Debug.Log("Fixed something"+FollowEye.FixedSomething);
        if (!eyeOnPanel)
        {

            //eyeOnPanel = false;
            ToPersonCamera.MainCamera.GetComponent<MoveRotationCamera>().moveByMouseSpeed = moveByMouseSpeed;
            descritption.gameObject.SetActive(false);
        }

    }

    void ShowPanelOnNPC() 
    {
       
        //Debug.Log("eyeOnNPC" + FollowEye.eyeOnNPC);
        if (ToPersonCamera.inNPCView && FollowEye.eyeOnNPC)
        {
            //Debug.Log("eyeOnNPC in NPC view");
            eyeOnSamllBuil = false;
            Vector3 temp2 = FollowEye.closestNpc.transform.position;
            temp2.y += 3f;
            Vector3 tarPosition = ToPersonCamera.activeCamera.WorldToScreenPoint(temp2);

            // ToPersonCamera.MainCamera.GetComponent<MoveRotationCamera>().moveByMouseSpeed = 0.1f;
            //Debug.Log("activePanel");
            descritption.gameObject.SetActive(true);
            descritption.gameObject.transform.position = tarPosition;
            descritption.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = FollowEye.closestNpc.name;
            //Debug.Log(descritption.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            //Debug.Log(descritption.gameObject.active);
            descritption.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            


            float x = Mathf.Abs(cursor.gazeScreenPosition.x - tarPosition.x);
            float y = Mathf.Abs(cursor.gazeScreenPosition.y - tarPosition.y);
            //Debug.Log(x + " " + y + " " + descritption.GetComponent<RectTransform>().sizeDelta.x / 2 + " " +  descritption.GetComponent<RectTransform>().sizeDelta.y / 2);

            if (x < descritption.GetComponent<RectTransform>().sizeDelta.x / 1920 * Screen.width / 2 + gazeMouse.errorLen &&
                y < descritption.GetComponent<RectTransform>().sizeDelta.y / 1920 * Screen.height / 2 + gazeMouse.errorLen)
            {
                //Debug.Log("focus on pannel");
                //Debug.Log("hit Building" + hit.transform.name);
                eyeOnPanel = true;
            }
            else
            {
                eyeOnPanel = false;
                //Debug.Log("eyeOnpanel"+eyeOnPanel);
            }



        }
    }
    void FindParent() 
    {
        hitParent = hit.transform;
        while(hitParent.tag != "detail") 
        {
            hitParent = hitParent.parent;
            Debug.Log(hitParent.name + hitParent.tag);
        }

    }
    void ShowPanelOnSmallBuild() 
    {
        
       

        Ray ray = ToPersonCamera.activeCamera.ScreenPointToRay(gazeContro.gazeScreenPosition);
        
       // RaycastHit hitTemp;
        if (allowChange )
        {
            if (Physics.Raycast(ray, out hit, 1000))
            {
               
                if (hit.transform.gameObject.layer == 13)
                {
                    //排除非跟随模式详细信息
                    allowChange = false;
                    countTime = FollowEye.blinkTime * 5;
                    eyeOnSamllBuil = true;
                    FindParent();
                }
                
            }
            else
            {
               
                eyeOnSamllBuil = false;
            }
        }

        if (eyeOnSamllBuil) 
        {
            FollowEye.eyeOnBuil = false;
            FollowEye.chooseBuild = false;
            //FollowEye.eyeOnNPC = false;
            Description.eyeOnPanel = false;
        }
        
        if (ToPersonCamera.inNPCView && eyeOnSamllBuil)
        {
            Vector3 temp2 = hit.transform.position;
            temp2.y += 1.5f;
            Vector3 tarPosition = ToPersonCamera.activeCamera.WorldToScreenPoint(temp2);

            // ToPersonCamera.MainCamera.GetComponent<MoveRotationCamera>().moveByMouseSpeed = 0.1f;
            //Debug.Log("activePanel");
            descritption.gameObject.SetActive(true);
            descritption.gameObject.transform.position = tarPosition;
            if(hitParent.transform.parent.tag == "poster") 
            {
                descritption.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hitParent.GetComponent<DescriptText>().descriptionTexts[0];
                descritption.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hitParent.GetComponent<DescriptText>().descriptionTexts[1];

            }
            else 
            {
                if (hitParent.GetComponent<DescriptText>() == null)
                {
                    descritption.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                    descritption.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
                else 
                {
                    string[] ss = hitParent.GetComponent<DescriptText>().descriptionTexts;
                    descritption.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hitParent.GetComponent<DescriptText>().descriptionTexts[0];
                    descritption.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hitParent.GetComponent<DescriptText>().descriptionTexts[1];
                }
            }


            float x = Mathf.Abs(cursor.gazeScreenPosition.x - tarPosition.x);
            float y = Mathf.Abs(cursor.gazeScreenPosition.y - tarPosition.y);
            //Debug.Log(x + " " + y + " " + descritption.GetComponent<RectTransform>().sizeDelta.x / 2 + " " +  descritption.GetComponent<RectTransform>().sizeDelta.y / 2);

            if (x < descritption.GetComponent<RectTransform>().sizeDelta.x / 1920 * Screen.width / 2 + gazeMouse.errorLen &&
                y < descritption.GetComponent<RectTransform>().sizeDelta.y / 1920 * Screen.height / 2 + gazeMouse.errorLen)
            {
                //Debug.Log("focus on pannel");
                //Debug.Log("hit Building" + hit.transform.name);
                eyeOnPanel = true;
            }
            else
            {
                eyeOnPanel = false;
                //Debug.Log("eyeOnpanel"+eyeOnPanel);
            }



        }
    }
}

