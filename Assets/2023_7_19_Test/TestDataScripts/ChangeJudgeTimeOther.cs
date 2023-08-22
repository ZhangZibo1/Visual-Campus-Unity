using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeJudgeTimeOther : MonoBehaviour
{
    // Start is called before the first frame update
    public float fixedTimeOther;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        FollowEye.EmptyLookTime = transform.GetComponent<upDownBar>().ChangedPara;
        fixedTimeOther = FollowEye.EmptyLookTime;

    }
    

}
