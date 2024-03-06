using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHook : MonoBehaviour
{
    LineRenderer lr;
    DistanceJoint2D dj2d;
    Vector2 MousePos;
    // Start is called before the first frame update
    void Start()
    {
        lr= GetComponent<LineRenderer>();
        dj2d=GetComponent<DistanceJoint2D>();
        dj2d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MouseUpdate();
    }

    private void MouseUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            MousePos= Input.mousePosition;
        }
    }
}
