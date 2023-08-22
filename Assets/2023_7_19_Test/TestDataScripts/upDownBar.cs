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
    public string unit;
    Slider slider;

    Button upButton;
    Button downButton;
    public void up() 
    {
        if(ChangedPara + step > max) { return; }
        ChangedPara += step;
        slider.value = (ChangedPara - min) / (max - min);
    }
    public void down() 
    {
        if (ChangedPara + step < min) { return; }
        ChangedPara -= step;
        slider.value = (ChangedPara - min) / (max - min);
    }
    public void slide() 
    {
        ChangedPara = (min + (max - min) * slider.value);
    }
    
        // Start is called before the first frame update
        void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        Debug.Log(slider.name);
        slider.value = (ChangedPara - min) / (max - min);
        upButton = GameObject.Find("upButton").GetComponent<Button>();
        downButton = GameObject.Find("downButton").GetComponent<Button>();
        upButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ " + step + unit;
        downButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " - " + step +unit;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "" + ChangedPara + unit;
    }
}
