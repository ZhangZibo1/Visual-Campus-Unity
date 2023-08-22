using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotationCamera : MonoBehaviour
{
    public  int moveSpeed = 30;
    int centerMotionSpeed = 10;
    Vector3 moveStep = new Vector3(20,20,20);
    public float moveByMouseSpeed = 1f;
    
    Vector3 moveDirec;
    Vector3 recordedmoveDirec = Vector3.zero;
    Vector3 reverseCount = Vector3.zero;

    Vector3 rotateDirec = Vector3.zero;
    Vector3 recordedrotateDirec = Vector3.zero;
    Vector3 reverseRotateCount = Vector3.zero;
    List<Transform> positionLists;
    public GazeController gazeContro;

    public float standView = 60;
    public static int partInt;
    public static int CameraNum = 1;
    void Start()
    {
        //moveSpeed = 10;
        moveStep = new Vector3(20,20,20);
        //moveByMouseSpeed = 0.2f;
        moveDirec = new Vector3(0, 0, 0);
        positionLists =new List<Transform>(GameObject.Find("CameraPositions").transform.GetComponentsInChildren<Transform>());
        positionLists.Remove(positionLists[0]);
        gazeContro = GameObject.Find("GazeController").GetComponent<GazeController>();
        partInt = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.F)) 
        {
            FollowEye.FixedSomething = false;
            FollowEye.fixedCountTime = 0;
            //Debug.Log("press");
        }

        float value = 0;
        if(standView  >= 75) 
        {
            standView = 75;
        }
        if(standView <= 10) 
        {
            standView = 10;
        }
        float mouseCenter = Input.GetAxis("Mouse ScrollWheel");
        if (mouseCenter < 0)
        {
            standView += Time.deltaTime * 60;
        }
        if(mouseCenter > 0) 
        {
            standView -= Time.deltaTime * 60;
        }
        if (FollowEye.FixedSomething == true)
        {
            if (FollowEye.chooseBuild) { value = standView*2/3;  }
            else { value =standView*2/3; }

            if (Description.eyeOnPanel)
            {
                value = standView*2/3;
            }
        }
        else
        {
            value = standView;
        }
        if(Mathf.Abs(GetComponent<Camera>().fieldOfView - value) > 1f) 
        {
            GetComponent<Camera>().fieldOfView = 0.9f * GetComponent<Camera>().fieldOfView + 0.1f * value;
        }
        
        //Debug.Log(GetComponent<Camera>().fieldOfView);
        //Debug.Log(gameObject.name);
        //check whether the mouse in the boundary of screen and move the camera position to change the camera view
        MoveByMouse();

        //Vector3 temp = Input.GetAxis("Move Forward") * transform.forward +
        //    Input.GetAxis("Move Vertical") * transform.up + Input.GetAxis("Move Right") * transform.right;
        //GetComponent<Rigidbody>().velocity = moveSpeed * temp;

        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"), Space.World);
        //transform.Rotate(-Vector3.right * Input.GetAxis("Mouse Y"), Space.Self);

        //Scale the camera field view;

        //the 

        MoveInputCheck();

        if(recordedmoveDirec != moveDirec*moveSpeed)
        {
            reverseCount = moveDirec*moveSpeed;
            recordedmoveDirec = moveDirec*moveSpeed;
        }
        if(reverseCount.magnitude > 0.1f)
        {
            //Debug.Log(reverseCount+" "+recordedmoveDirec);
            transform.Translate(recordedmoveDirec/centerMotionSpeed, Space.World);
            reverseCount -= recordedmoveDirec/ centerMotionSpeed;
            //Debug.Log(reverseCount + " " + recordedmoveDirec);
        }
        else
        {
            moveDirec = Vector3.zero;
            recordedmoveDirec = Vector3.zero;
        }


        ///pay attention in default the move of translate is respective to self

        rotateInputCheck();
        if (reverseRotateCount.magnitude > 0.001f)
        {
            transform.Rotate(recordedrotateDirec/centerMotionSpeed, Space.World);
            reverseRotateCount -= recordedrotateDirec / centerMotionSpeed;
        }
        else
        {
            rotateDirec = Vector3.zero;
            recordedrotateDirec = Vector3.zero;
        }

        int num = CheckInputNum();
        if(num == 0)
        {
            //do nothing
        }
        else
        {
            CameraNum = num;

            transform.position = positionLists[num - 1].position;
            transform.rotation = positionLists[num - 1].rotation; 
        }
    }


     void MoveInputCheck()
    {
        if (recordedmoveDirec.magnitude > 0.001f) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log("press w");
            moveDirec += Quaternion.AngleAxis(90, Vector3.down) * transform.right.normalized;
        }//获得和right相对应的水平正前方位置
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Debug.Log("press s");
            moveDirec -= Quaternion.AngleAxis(90, Vector3.down) * transform.right.normalized;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log("press d");
            moveDirec += transform.right.normalized;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("press a");
            moveDirec -= transform.right.normalized;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("press q");
            moveDirec -= Vector3.up.normalized;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Debug.Log("press e");
            moveDirec += Vector3.up.normalized;
        }
    }
    void rotateInputCheck()
    {
        if (recordedrotateDirec.magnitude <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))//control the rotation of camera by arrow keys.
            {
                //Debug.Log("press up");
                rotateDirec += -transform.right * 45;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rotateDirec += transform.right * 45;
            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                rotateDirec += Vector3.up * 45;
            }
            if (Input.GetKeyDown(KeyCode.Q)|| Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rotateDirec += -Vector3.up * 45;
            }
            if (recordedrotateDirec != rotateDirec)
            {
                recordedrotateDirec = rotateDirec;
                reverseRotateCount = rotateDirec;
            }
        }
    }

     void MoveByMouse()
    {
        Vector3 mP = FollowEye.position;
        float ScreenWidth = Screen.width;
        float ScreenHeight = Screen.height;
        Vector3 MoveDirForMouse = Vector3.zero;
        if (mP[0] < ScreenWidth / partInt)
        {
            MoveDirForMouse -= transform.right.normalized * (ScreenWidth / partInt - mP[0]);
        }
        if (mP[0] > ScreenWidth * (partInt -1)/ partInt)
        {
            MoveDirForMouse += transform.right.normalized * (mP[0] - ScreenWidth * (partInt - 1) / partInt);
        }
        if (mP[1] < ScreenHeight / partInt)
        {
            MoveDirForMouse -= transform.up.normalized * (ScreenHeight / partInt - mP[1]);
        }
        if (mP[1] > ScreenHeight * (partInt - 1) / partInt)
        {
            MoveDirForMouse += transform.up.normalized * (mP[1] - ScreenHeight * (partInt - 1) / partInt);
        }
        GetComponent<Rigidbody>().velocity = MoveDirForMouse * moveByMouseSpeed;
    }

    int CheckInputNum()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 6;
        }
        return 0;
    }
}
