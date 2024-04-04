using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallControl : MonoBehaviour
{
    StopTime stopTime;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        stopTime=GetComponent<StopTime>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
