using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraMove : MonoBehaviour
{
    public Transform player;

    private Vector3 offset;

    public float smoothing = 300f;
    private void Start()
    {

        //player = GameObject.FindGameObjectWithTag("Player").transform;

        offset = transform.position - player.position;
    }
    private void LateUpdate()
    {

        if(player!=null)
        {
            transform.position = Vector3.Lerp(transform.position,player.position+offset,smoothing);
        }
    }
}
