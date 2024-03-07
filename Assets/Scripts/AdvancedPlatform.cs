using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPlatform : MonoBehaviour
{
    private GameObject currentPlatform=null;
    private CapsuleCollider2D playerCollider; 
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
