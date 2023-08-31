using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateNPC : MonoBehaviour
{
    int centerMotionSpeed = 10;
    Vector3 rotateDirec = Vector3.zero;
    Vector3 recordedrotateDirec = Vector3.zero;
    Vector3 reverseRotateCount = Vector3.zero;
    // Start is called before the fi
    // rst frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateInputCheck();
        if (reverseRotateCount.magnitude > 0.001f)
        {
            transform.Rotate(recordedrotateDirec / centerMotionSpeed, Space.World);
            reverseRotateCount -= recordedrotateDirec / centerMotionSpeed;
        }
        else
        {
            rotateDirec = Vector3.zero;
            recordedrotateDirec = Vector3.zero;
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
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow))
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
}
