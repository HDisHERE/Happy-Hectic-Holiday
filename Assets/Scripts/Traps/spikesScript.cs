using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesScript : MonoBehaviour
{
    PlayerControl playerControl;

    private void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerControl.PlayerDeath();
        }
    }
}
