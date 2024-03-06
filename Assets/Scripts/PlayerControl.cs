using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Basic Data
    //Walk
    Rigidbody2D rb;

    public Transform playerSpawnPoint;

    CanvasHandlerScript canvasToggle;

    public float moveSpeed = 2.5f;
    private float x;

    //Jump
    [Range(0f, 30f)]
    public float jumpSpeed = 30.0f;
    private bool isJumping=false;

    //Better Jump
    public float fallAdd;
    public float jumpAdd;
    private bool isHoldingJump=false;

    //Determines whether player is dead or alive
    bool dead;

    private Transform groundTf;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        groundTf=transform.Find("Ground");
        canvasToggle=GetComponentInChildren<CanvasHandlerScript>();
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        //Here is everything about input.
        if (!dead)
        {
            x = Input.GetAxis("Horizontal");//Get input every frame.
            isJumping = Input.GetButton("Jump");
            isHoldingJump = Input.GetButton("Jump");
        }

        if (isOnGround() && isJumping)
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);//One way for jumping. May change for optimization later
            rb.velocity = Vector2.up * jumpSpeed;
        }


        if (Input.GetKeyDown(KeyCode.Space) && dead == true)
        {
            Respawn();
            Debug.Log("Respawn attempt");
        }

    }
        //if the player is dead, respawn them when they press space
        
    
    
    private void FixedUpdate()
    {
        //Here is everything about physical calculation.
        if (!dead)
        {
            PosUpdate();
            JumpUpdate();
        }
    }

    private void PosUpdate()
    {
        if (x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);//Turn around the sprite. 
        }

        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        //Here I choose to change the velocity directly.
        //Please notice that I changed the velocity directly instead of using force system. And there is a material
        //That erase all the friction on the wall to avoid bug that stick player to the wall, which means that
        //FORCE SYSTEM CAN'T BE USED TO CONTROL THE MOVEMENT IN THIS PROJECT. Otherwise everything is sliding.
    }

    private void JumpUpdate()
    {
        //Hereis the code to optimize jumping
        if(rb.velocity.y < 0) //When Falling
        {
            //The default gravity is 1.Thus here I need to decrease 1 to get the true gravity speed.
            rb.velocity += Vector2.up* Physics2D.gravity.y*(fallAdd-1)*Time.fixedDeltaTime;
        }
        else if(rb.velocity.y > 0&&!isHoldingJump)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpAdd - 1) * Time.fixedDeltaTime;
        }
    }

    public bool isOnGround()
    {
        //Check if the player is on the ground by detecting distance of the ground point pos and ground.
        return Physics2D.OverlapCircle(groundTf.position, 0.02f, LayerMask.GetMask("ground"));
    }
    

    public void Respawn()
    {
        canvasToggle.ToggleCanvasOff();
        if (playerSpawnPoint !=null)
        {
            rb.isKinematic= false;
            dead = false;
            transform.position = playerSpawnPoint.position;
        }
    }

    public void PlayerDeath()
    {
        canvasToggle.ToggleCanvasOn();
        rb.velocity = Vector2.zero;
        rb.isKinematic= true;
        dead = true;
    }
}
