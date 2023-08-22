using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPersonCamera : MonoBehaviour
{
    public Camera[] cameraList;
    public static AutoNPCController[] NPCs;
    public static Camera activeCamera = null;
    public static Camera MainCamera;
    
    public Transform gazeObject;
    public GazeController gazeContro;
    public bool escape = false;
    public GameObject escUI;
    public static bool inNPCView =false;

    Quaternion npcRotation;
    public static Vector3 eyeHitPoint = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
        //cameraList = GameObject.FindObjectsOfType<Camera>();
        NPCs = GameObject.FindObjectsOfType<AutoNPCController>();
        MainCamera = gameObject.GetComponent<Camera>();
        activeCamera = MainCamera;
        activeCamera = MainCamera;
        gazeContro = GameObject.Find("GazeController").GetComponent<GazeController>();
    }

    // Update is called once per frame
    void Update()
    {

        //gazeFollow();
        //Debug.Log(gameObject.name);
        //Debug.Log(gazeContro.gazeScreenPosition);

        if (FollowEye.eyeOnNPC && FollowEye.chooseNPC && !inNPCView)
        {
            inNPCView = true;
            activeCamera = FollowEye.closestNpc.transform.GetChild(2).GetComponent<Camera>();
            //Debug.Log(Screen.height+"  ,, "+Screen.width+"  ,, "+ Input.mousePosition);
            MainCamera.enabled = false;
            MainCamera.GetComponent<MoveRotationCamera>().enabled = false;
            activeCamera.enabled = true;
            activeCamera.GetComponent<RotateNPC>().enabled = true;
            npcRotation = activeCamera.transform.rotation;
            //Debug.Log("chooseNPC once");

            FollowEye.FixedSomething = false;
            FollowEye.fixedCountTime = 0;
            FollowEye.FixedSomething = false;

            escUI.SetActive(true);
        }


        if (escape)
        {
            inNPCView = false;
            if (activeCamera != null)
            {
                activeCamera.enabled = false;
                MainCamera.enabled = true;
                MainCamera.GetComponent<MoveRotationCamera>().enabled = true;
                activeCamera.transform.rotation = npcRotation;
                activeCamera.GetComponent<RotateNPC>().enabled = false;
                activeCamera = MainCamera;// exit the personal camera back to mainCamera
            } 
            escape = false;
            escUI.SetActive(false);
        }

        if (inNPCView)
        {
            
        }
        if(Input.GetKeyDown(KeyCode.Escape) == true) 
        {
            ESC(); 
        }


    }

    public void ESC()
    {
        escape = true;
    }
    void gazeFollow()
    {
        Ray ray2 = MainCamera.ScreenPointToRay(gazeContro.gazeScreenPosition);//click to enter the personal camera
        //Debug.DrawRay(ray2.origin,ray2.direction, Color.black,1000); 
        //Debug.Log(ray.direction);
        RaycastHit hit2;
        if (Physics.Raycast(ray2, out hit2, 1000))
        {
            //Debug.Log("hit something");
            gazeObject.transform.position = hit2.point;
            eyeHitPoint = hit2.point;
            //Debug.Log(hit2.transform.position);
        }
    }
}
  

