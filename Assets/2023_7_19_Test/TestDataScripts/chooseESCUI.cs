using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chooseESCUI : MonoBehaviour
{

    public FollowEye followEye;
    public static bool eyeOnEsc = false;
    float time;
    float utime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Abs(followEye.transform.position.x - transform.position.x);
        float y = Mathf.Abs(followEye.transform.position.y - transform.position.y);
        if (x < transform.GetComponent<RectTransform>().sizeDelta.x / 1920 * Screen.width / 2 + followEye.errorLen/2 &&
                y < transform.GetComponent<RectTransform>().sizeDelta.y / 1920 * Screen.height / 2 + followEye.errorLen/2) 
        
        {

            eyeOnEsc = true;
            time += Time.deltaTime;
            utime = 0;
        }
        else 
        {
            eyeOnEsc = false;
            time = 0;
        }
        
        
            if (FollowEye.FixedSomething == true )
        {
            
            if ( time/FollowEye.FixedChooseTime > 0.8)
            { 
                //Debug.Log("press esc");
                gameObject.GetComponent<Button>().onClick.Invoke();
                FollowEye.FixedSomething = false;
                FollowEye.fixedCountTime = 0;
                FollowEye.FixedSomething = false;

                Vector3 temp = followEye.transform.position;
                temp.x = Screen.width / 2;
                temp.y = Screen.height / 2;
                followEye.transform.position = temp;
            }
        }
    }
}
