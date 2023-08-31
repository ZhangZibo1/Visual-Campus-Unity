using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NewBehaviourScript : MonoBehaviour
{
    float count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float temp = 0;
        if(FollowEye.eyeOnBuil || FollowEye.eyeOnNPC || Description.eyeOnPanel) 
        {
            temp = FollowEye.FixedChooseTime;
        }
        else 
        {
            temp = FollowEye.EmptyLookTime;
            if(count > temp) 
            {
                count = temp;
                    }
        }

        if (FollowEye.fixedCountTime >= temp)
        {
            count = temp;
        }
        else
        {
            count = FollowEye.fixedCountTime;
        }
        GetComponent<TextMeshProUGUI>().text = "" + Mathf.Floor(10 * count / temp) / 10;
    }
}
