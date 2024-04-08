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
        rb.velocity=new Vector2 (rb.velocity.x,rb.velocity.y);

      if(rb.velocity.y > 0) 
       {
            rb.AddForce(new Vector2(0f, 0.02f), ForceMode2D.Impulse); 
       }
    }
}
