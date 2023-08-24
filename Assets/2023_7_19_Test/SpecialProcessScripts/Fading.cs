using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public float fadingPercentage = 0.3f;

    Color orgiCImage;
    Color origColorT;
    Image im;
    TextMeshProUGUI te;
    // Start is called before the first frame update
    void Start()
    {
        orgiCImage = GetComponent<Image>().color;
        origColorT = transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        im = GetComponent<Image>();
        te = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Description.eyeOnPanel)
        {
            im.color = orgiCImage * fadingPercentage;
            te.color = origColorT * fadingPercentage;
        }
        else 
        {
            im.color = orgiCImage;
            te.color = origColorT;
        }
        
    }
}
