using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointScript : MonoBehaviour
{
    PlayerLife PlayerLife;
    // Start is called before the first frame update
    void Start()
    {
        PlayerLife = FindObjectOfType<PlayerLife>();
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerLife.playerSpawnPoint = this.gameObject.transform;
        }
    }
}
