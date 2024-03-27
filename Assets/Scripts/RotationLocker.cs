using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLocker : MonoBehaviour
{
    Quaternion myRotation;
    // Start is called before the first frame update
    void Start()
    {
        myRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = myRotation;
    }
}
