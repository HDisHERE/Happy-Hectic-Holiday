using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHook : MonoBehaviour
{
    LineRenderer lr;
    DistanceJoint2D dj2d;
    Vector2 MousePos;
    bool isHooking;
    // Start is called before the first frame update
    void Start()
    {
        lr= GetComponent<LineRenderer>();
        dj2d=GetComponent<DistanceJoint2D>();
        lr.enabled = false;
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
            MousePos=(Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);//Transfer the pos on camera to global pos.
            lr.enabled = true;
            dj2d.enabled= true;
            lr.SetPosition(0, MousePos);
            lr.SetPosition(1,transform.position);
            dj2d.connectedAnchor = MousePos;
        }
        lr.SetPosition(1,transform.position);
    }
}
