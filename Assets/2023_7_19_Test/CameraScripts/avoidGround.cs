using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avoidGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        if(transform.position.x >= 190) 
        {
            temp.x = 190;
        }
        if(transform.position.y <= 10) 
        {
            temp.y = 10;
        }
        if (transform.position.x <= -190)
        {
            temp.x = -190;
        }
        if(transform.position.z <= -190) 
        {
            temp.z = -190;
        }
        if (transform.position.z >= 190)
        {
            temp.z = 190;
        }

        if(transform.position.y >= 200) 
        {
            temp.y = 200;
        }
        transform.position = temp;


    }
}
