using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Basic Data
    //Walk
    Rigidbody2D rb;
    //public float moveSpeed = 2.5f;
    public float moveForce = 2.5f;
    private float Inputx;

    public float maxSpeed;
    public float linerDrag;

    //Jump
    [Range(0f, 30f)]
    //public float jumpSpeed = 30.0f;
    public float jumpForce = 8.0f;
    private bool isJumping=false;

    //Better Jump
    public float fallAdd;
    public float jumpAdd;
    private bool isHoldingJump=false;

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
        //Here is everything about input.
        //Inputx = Input.GetAxis("Horizontal");//-1~1//Get input every frame.
        Inputx=Input.GetAxisRaw("Horizontal");
        isJumping = Input.GetButton("Jump");
        isHoldingJump = Input.GetButton("Jump");

        if (isOnGround() && isJumping)
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);//One way for jumping. May change for optimization later
            //rb.velocity = Vector2.up * jumpSpeed;//When using this code, the camera slightly shakes when the player
            //drops to the ground. So I choose to use addforce for better experience.
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //Here is everything about physical calculation.
        PosUpdate();
        JumpUpdate();
        linerDragUpdate();
    }

    private void PosUpdate()
    {
        if (Inputx != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(Inputx), 1, 1);//Turn around the sprite. 
        }

        //rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        //Here I choose to change the velocity directly.
        //Please notice that I changed the velocity directly instead of using force system. And there is a material
        //That erase all the friction on the wall to avoid bug that stick player to the wall, which means that
        //FORCE SYSTEM CAN'T BE USED TO CONTROL THE MOVEMENT IN THIS PROJECT. Otherwise everything is sliding.

        //Updated: I find a way to control the friction, and I found some problems when changing the velocity directly:
        //When the player moves too fast, the player may go across the wall. So considering the trade off, I choose to use force system instead.
        rb.AddForce(new Vector2(moveForce* Inputx, 0f),ForceMode2D.Impulse);
        if(Mathf.Abs(rb.velocity.x)>maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);//Get the real time speed.
        }
    }

    private void linerDragUpdate()
    {
        if((Mathf.Abs(Inputx)<0.1f)&&isOnGround())
        {
            rb.AddForce(new Vector2( Mathf.Sign(rb.velocity.x) * (-linerDrag),0f));
        }

        else
        {
            return;
        }
    }

    private void JumpUpdate()
    {
        //Hereis the code to optimize jumping
        if(rb.velocity.y < 0) //When Falling
        {
            //The default gravity is 1.Thus here I need to decrease 1 to get the true gravity speed.
            //rb.velocity += Vector2.up* Physics2D.gravity.y*(fallAdd-1)*Time.fixedDeltaTime; Change the gravity scale is much better
            rb.gravityScale = fallAdd;
        }
        else if(rb.velocity.y > 0&&!isHoldingJump)
        {
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpAdd - 1) * Time.fixedDeltaTime;
            rb.gravityScale = jumpAdd;
        }
        else 
        {
            rb.gravityScale = 1;
        }
    }

    public bool isOnGround()
    {
        //Check if the player is on the ground by detecting distance of the ground point pos and ground.
        return Physics2D.OverlapCircle(groundTf.position, 0.02f, LayerMask.GetMask("ground"));
    }
}
