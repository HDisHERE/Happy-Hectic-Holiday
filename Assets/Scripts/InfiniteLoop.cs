using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLoop : MonoBehaviour
{
    GameObject mainCam;
    float width = 20.0f;
    float totalwidth;
    int units=2;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        totalwidth = width * units;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos= transform.position;
        if(mainCam.transform.position.x>transform.position.x+totalwidth/2.0f)
        {
            pos.x += totalwidth;
            transform.position = pos;
        }
        else if(mainCam.transform.position.x<transform.position.x-totalwidth/2.0f)
        {
            pos.x -= totalwidth;
            transform.position = pos;
        }
    }
}
