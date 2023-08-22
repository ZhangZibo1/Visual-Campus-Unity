using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class changeBlinkTime : MonoBehaviour
{
    public float BlinkTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BlinkTime = transform.GetComponent<upDownBar>().ChangedPara;
        FollowEye.ChangeBlind(BlinkTime);
    }
    
}
