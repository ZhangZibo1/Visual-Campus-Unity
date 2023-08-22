using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HideDisplayHint : MonoBehaviour
{
    // Start is called before the first frame update
    bool active = true;
    bool pressedRecent = false;
    float ReverseCount = 0;
    void Start()
    {
        ReverseCount = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
        if(FollowEye.FixedSomething && Vector2.Distance(FollowEye.target, transform.position) < 50 && !pressedRecent)
        {
            GetComponent<Button>().onClick.Invoke();
            pressedRecent = true;
            ReverseCount = 2;
        }
        if(pressedRecent == true)
        {
            ReverseCount -= Time.deltaTime;
        }
        if(ReverseCount <= 0)
        {
            pressedRecent = false;
        }

    }
    public void ChangeHintState()
    {
        if(active)
        {
            active = false;
            transform.GetChild(1).gameObject.SetActive(false);
            Debug.Log(transform.GetChild(0).name);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Show Hint";
               
        }
        else
        {
            active = true;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hide Hint";
        }
    }
}
