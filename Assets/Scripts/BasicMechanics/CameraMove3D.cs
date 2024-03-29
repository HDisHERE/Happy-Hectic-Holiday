using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMove3D : MonoBehaviour
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
        
        Vector3 targetPosition = player.position + player.TransformDirection(offset);

        float x = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing).x;
        float y = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing).y;
        transform.position = new Vector3(x, y, -10);


        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);

        //transform.LookAt(player.position);
    }
}
