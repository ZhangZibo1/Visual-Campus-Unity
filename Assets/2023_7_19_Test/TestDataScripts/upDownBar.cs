using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class upDownBar : MonoBehaviour
{
    public float ChangedPara;
    public float step;
    public float max;
    public float min;
    public float accuracy;
    public string unit;
    Slider slider;

    Button upButton;
    Button downButton;
    public void up() 
    {
        if(ChangedPara + step > max) { ChangedPara = max; return; }
        ChangedPara += step;
        ChangedPara = Mathf.Round (ChangedPara * accuracy) / accuracy;


        slider.value = (ChangedPara - min) / (max - min);
    }
    public void down() 
    {
        if (ChangedPara - step < min) { ChangedPara = min; return; }
        ChangedPara -= step;
        ChangedPara = Mathf.Round(ChangedPara * accuracy) / accuracy;
        slider.value = (ChangedPara - min) / (max - min);
    }
    //public void slide() 
    //{
    //   ChangedPara = (min + (max - min) * slider.value);
    //}
    
        // Start is called before the first frame update
        void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        //Debug.Log(slider.name);
        if(gameObject.scene.name == "ErrorRadius") 
        {
            ChangedPara = FollowEye.errorLen;
        }
        if (gameObject.scene.name == "BlinkTime")
        {
            ChangedPara = FollowEye.blinkTime;
        }
        if (gameObject.scene.name == "JudegeTime(building, UI,chara)")
        {
            ChangedPara = FollowEye.FixedChooseTime;
        }
        if (gameObject.scene.name == "JudegeTime(other)")
        {
            ChangedPara = FollowEye.EmptyLookTime;
        }

        slider.value = (ChangedPara - min) / (max - min);
        upButton = GameObject.Find("upButton").GetComponent<Button>();
        downButton = GameObject.Find("downButton").GetComponent<Button>();
        //upButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ " + step + unit;
        //downButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " - " + step +unit;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "" + Mathf.FloorToInt(ChangedPara * 1000)/1000f + unit;
    }
}
