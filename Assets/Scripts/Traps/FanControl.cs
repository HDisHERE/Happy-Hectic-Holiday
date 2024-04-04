using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControl : MonoBehaviour
{
    StopTime stopTime;
    AreaEffector2D effector;
    private float originForce;
    // Start is called before the first frame update
    void Start()
    {
        stopTime=GetComponent<StopTime>();
        effector = GetComponent<AreaEffector2D>();
        originForce = effector.forceMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopTime.isStoped)
        {
            effector.forceMagnitude = 0f;
        }
        else
        {
            effector.forceMagnitude = originForce;
        }
    }
}
