using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Basic Data
    Rigidbody2D rb;

    public Transform playerSpawnPoint;

    public float moveSpeed = 2.5f;
    public float jumpSpeed = 30.0f;

    //Determines whether player is dead or alive
    bool dead;

    private Transform groundTf;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        groundTf=transform.Find("Ground");
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is dead, respawn them when they press space
        if (Input.GetKeyDown(KeyCode.Space) && dead == true)
        {
            Respawn();
        }

    }
    
    private void FixedUpdate()
    {
        PosUpdate();
    }

    public void PosUpdate()
    {
        float x = Input.GetAxis("Horizontal");

        if (x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);//Turn around the sprite. 
        }

        if (isOnGround() && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);//One way for jumping. May change for optimization later
        }

        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);//Here I choose to change the velocity directly.
    }

    public bool isOnGround()
    {
        //Check if the player is on the ground by detecting distance of the ground point pos and ground.
        return Physics2D.OverlapCircle(groundTf.position, 0.02f, LayerMask.GetMask("ground"));
    }
    

    public void Respawn()
    {
        if(playerSpawnPoint !=null)
        {
            dead = false;
            transform.position = playerSpawnPoint.position;
        }
    }

    public void PlayerDeath()
    {
        dead = true;
    }
}
