using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangePara : MonoBehaviour
{
    
    
    public float fixedTime;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        FollowEye.FixedChooseTime = transform.GetComponent<upDownBar>().ChangedPara;
        fixedTime = FollowEye.FixedChooseTime;

    }
   
    
}
