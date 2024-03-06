using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    PlayerControl playerControl;

    //speed of the arrow
    float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerControl= FindObjectOfType<PlayerControl>();

        //moves the arrow
        rigidBody.velocity = transform.up*moveSpeed;
    }

    //detects when the arrow comes in contact with an object, checks if it is the player, then destroys the arrow
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerControl.PlayerDeath();
            Debug.Log("arrow hit player");
        }
        Destroy(gameObject);
    }



}
