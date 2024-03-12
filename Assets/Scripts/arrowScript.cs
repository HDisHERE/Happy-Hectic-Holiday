using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    PlayerControl playerControl;

    //speed of the arrow
    public float arrowSpeed = 10;
    public bool isVertical = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerControl= FindObjectOfType<PlayerControl>();

        //moves the arrow
        if(isVertical)
        {
            rigidBody.velocity = transform.up * arrowSpeed;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            //rigidBody.velocity = transform.right * (-arrowSpeed);
            rigidBody.velocity = transform.up * arrowSpeed;
        }
        
    }

    //detects when the arrow comes in contact with an object, checks if it is the player, then destroys the arrow
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerControl.PlayerDeath();
            Debug.Log("arrow hit player");
        }
        if(!collision.gameObject.CompareTag("grate"))
        {
            Destroy(gameObject);
        }
        
    }



}
