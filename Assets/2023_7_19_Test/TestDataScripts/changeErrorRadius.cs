using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class changeErrorRadius : MonoBehaviour
{
    public float errL;
    FollowEye gazeMouse;
    // Start is called before the first frame update
    void Start()
    {
        
        gazeMouse = GameObject.FindObjectOfType<FollowEye>();
    }

    // Update is called once per frame
    void Update()
    {
        errL = transform.GetComponent<upDownBar>().ChangedPara;
        gazeMouse.changeErrorR(errL);
    }
    
}
