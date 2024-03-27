using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointScript : MonoBehaviour
{

    PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            playerControl.playerSpawnPoint = this.gameObject.transform;
        }
    }
}
