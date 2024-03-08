using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMove : MonoBehaviour
{
    
    private Transform player;
    
    private Vector3 offset;
    
    private float smoothing = 3f;
    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        offset = transform.position - player.position;
    }
    private void LateUpdate()
    {
        
        Vector3 targetPosition = player.position + player.TransformDirection(offset);
       
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
        
        transform.LookAt(player.position);
    }
}
