using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesScript : MonoBehaviour
{
    PlayerLife PlayerLife;

    private void Start()
    {
        PlayerLife = FindObjectOfType<PlayerLife>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLife.PlayerDeath();
        }
    }
}
