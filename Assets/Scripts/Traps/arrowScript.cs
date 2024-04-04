using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    PlayerLife PlayerLife;

    //speed of the arrow
    public float arrowSpeed = 10;
    public bool isVertical = true;

    StopTime stopTime;

    // Start is called before the first frame update
    void Start()
    {
        stopTime = GetComponent<StopTime>();

        rigidBody = GetComponent<Rigidbody2D>();
        PlayerLife = FindObjectOfType<PlayerLife>();

    }

    private void Update()
    {
        arrowMove();
    }

    private void arrowMove()
    {
        //moves the arrow

        if (!stopTime.isStoped)
        {
            if (isVertical)
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
    }

    //detects when the arrow comes in contact with an object, checks if it is the player, then destroys the arrow
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerLife.PlayerDeath();
            Debug.Log("arrow hit player");
        }
        if(!collision.gameObject.CompareTag("grate"))
        {
            Destroy(gameObject);
        }
        
    }



}
