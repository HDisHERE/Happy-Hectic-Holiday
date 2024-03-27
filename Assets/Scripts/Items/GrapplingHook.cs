using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private Joint2D SpringJoint;
    Vector2 hookPos;
    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        SpringJoint = GetComponent<SpringJoint2D>();
        line= GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            hookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

           Collider2D col= Physics2D.OverlapPoint(hookPos);

            if(col)
            {
                SpringJoint.connectedBody=col.GetComponent<Rigidbody2D>();
            }
        }

        Vector3 dir = (SpringJoint.connectedBody.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle-90);

        line.SetPosition(0,transform.position);
        line.SetPosition(1, SpringJoint.connectedBody.transform.position);
    }
}
