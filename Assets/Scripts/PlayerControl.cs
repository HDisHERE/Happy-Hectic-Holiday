using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Basic Data
    //Walk
    Rigidbody2D rb;
    public float moveSpeed = 2.5f;

    //Jump
    [Range(0f, 30f)]
    public float jumpSpeed = 30.0f;
    private bool isJumping=false;
    private float x;

    private Transform groundTf;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        groundTf=transform.Find("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");//Get input every frame.
        isJumping = Input.GetButton("Jump");

        if (isOnGround() && isJumping)
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);//One way for jumping. May change for optimization later
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }

    private void FixedUpdate()
    {
        PosUpdate();
    }

    public void PosUpdate()
    {
        if (x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);//Turn around the sprite. 
        }

        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);//Here I choose to change the velocity directly.
        //Please notice that I changed the velocity directly instead of using force system. And there is a material
        //That erase all the friction on the wall to avoid bug that stick player to the wall, which means that
        //FORCE SYSTEM CAN'T BE USED TO CONTROL THE MOVEMENT IN THIS PROJECT. Otherwise everything is sliding.
    }

    public bool isOnGround()
    {
        //Check if the player is on the ground by detecting distance of the ground point pos and ground.
        return Physics2D.OverlapCircle(groundTf.position, 0.02f, LayerMask.GetMask("ground"));
    }
}
