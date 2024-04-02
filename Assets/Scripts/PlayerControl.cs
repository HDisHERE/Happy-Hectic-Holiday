using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Basic Data
    //Animation
    Animator ani;
    private Transform groundTf;
    private Transform leftWallTf;
    private Transform rightWallTf;
    private Transform KillTf;
    LayerMask enemyMask;
    LayerMask groundMask;
    LayerMask platformMask;

    //Run

    Rigidbody2D rb;
    [Header("Player Run:")]
    [Range(0f, 50f)]
    private bool isRunning;
    public float currentSpeed;
    public float runSpeed = 16f;
    private float originRunSpeed;
    //public float dashForce = 2.5f;
    private float Inputx;
    private float movePress;

    public bool isTurnRight = false;

    //With Force system
    //public float moveForce;
    //public float maxSpeed;
    //public float groundFriction;

    //Dash
    [Header("Player Dash:")]
    //Double press to dash
    public bool DoublePress;
    private float leftPresstime;
    private float rightPresstime;
    public float maxWaittime;
    private float originDashSpeed;
    private bool canDash;
    private bool isDashing;
    public float dashSpeed;

    //Jump
    [Header("Player Jump:")]
    [Range(0f, 50f)]
    //public float jumpSpeed = 30.0f;
    public float jumpForce = 40.0f;
    private bool jumpPress = false;
    [Range(19f, 50f)]
    public float maxFallspeed;
    public float randomc;

    //Double jump
    [Header("Player Double Jump:")]
    public int maxJumpcount;
    public int jumpCount;
    private bool isJumping;

    //Better Jump
    [Header("Jump details:")]
    [Range(0f, 20f)]
    public float rbGrav;
    [Range(0f, 20f)]
    public float jumpAdd;
    [Range(0f, 20f)]
    public float fallAdd;
    //public float airFriction;
    private bool isHoldingJump = false;

    //Jump collision refine
    [Header("Collision refine:")]
    public float raycastLength;
    public Vector3 cornerRaycastPos;
    public Vector3 innerRaycastPos;
    public bool isCorrecting;



    //Crouch
    //[Header("Player Crouch:")]
    //public bool iscrouching;
    //public float crouchSpeed;

    //Dash
    /*[Header("Player Dash:")]
    private bool canDash=true;
    private bool isDashing=false;
    public float dashForce = 20.0f;
    public float dashTime = 0.2f;
    public float dashMaxspeed = 40f;*/

    //Items
    [Header("Items:")]
    //GrapplingHook
    [Header("GrapplingHook:")]
    Tutorial_GrapplingGun hookGun;
    public static bool hasGrapple;
    public bool isUsingHook;
    public GameObject GunPivot;

    //Shield
    [Header("Shield:")]

    public GameObject Shield;
    public float shieldDashSpeed;
    public static bool hasShield;

    [Header("StopWatch:")]

    public bool isStopping;
    public static bool hasWatch;

    [Header("Speed Shoes:")]

    public GameObject Shoes;
    public float shoesRunSpeed;
    public float shoesDashSpeed;
    public static bool hasShoes;

    //Death

    private PlayerLife playerLife;

    //sound
    [Header("Audio Resources")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        groundTf=transform.Find("Ground");
        leftWallTf = transform.Find("Leftwall");
        rightWallTf = transform.Find("Rightwall");
        KillTf = transform.Find("Killpoint");
        leftPresstime = rightPresstime = -maxWaittime;
        hookGun= GetComponent <Tutorial_GrapplingGun>();
        
        ani = GetComponent<Animator>();
        groundMask = LayerMask.GetMask("ground");
        platformMask = LayerMask.GetMask("platform");
        enemyMask = LayerMask.GetMask("enemy");


        audioSource = GetComponent<AudioSource>();

        playerLife= GetComponent<PlayerLife>();

        originDashSpeed = dashSpeed;

        originRunSpeed= runSpeed;

        currentSpeed = runSpeed;

        jumpCount = maxJumpcount;


        if (hasShield)
        {
            Shield.SetActive(true);
        }
        if (hasGrapple)
        {
            GunPivot.SetActive(true);

        }
        if(hasShoes)
        {
                Shoes.SetActive(true);
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        itemUpdate();

        if (DoublePress)
        {
            DashCheck();
        }
        else
        {
            originDashcheck();
        }


        if (jumpCount > 0 && jumpPress)
        {

            isJumping = true;
        }

        //Here is everything about input.

        if(!playerLife.dead)
        {
            getInput();
        }

        //Shift to dash
        /*if(Input.GetKey(KeyCode.LeftShift)&&canDash)
        {
            StartCoroutine(Dash());
        }*/
    }





    //if the player is dead, respawn them when they press space

    //play landing sound effect
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void FixedUpdate()
    {
        //Here is everything about physical calculation.
        if (!playerLife.dead)
        {
            Raycastcollision();
            PosUpdate();
            JumpUpdate();
            EnemyCheck();
        }
            //linerDrageUpdate();//Works with force system
            if (isCorrecting)
            {
                CornerCorrect(rb.velocity.y);
            }
        

    }

    private void LateUpdate()
    {
        if (isOnGround())
        {
            if (Mathf.Abs(rb.velocity.x) < 0.5)
            {
                ani.Play("playerIdle");
            }
            else
            {
                ani.Play("playerRun");
            }

        }

        else
        {
            ani.Play("playerJump");
        }
    }

    private void getInput()
    {
        Inputx = Input.GetAxis("Horizontal");//-1~1//Get input every frame.
        movePress = Input.GetAxisRaw("Horizontal");//-1,0,1//
        jumpPress = Input.GetButtonDown("Jump");
        isHoldingJump = Input.GetButton("Jump");
    }
    
    public void itemUpdate()
    {
        //Hook
        if(GetComponent<SpringJoint2D>().enabled==true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                isUsingHook = true;
                //grapplingHookUpdate();
            }
        }
        else
        {
            isUsingHook = false;
        }

        //Shield
        
        if(hasShield)
        {
            dashSpeed = shieldDashSpeed;
        }
        else
        {


            dashSpeed =originDashSpeed;
        }

        //Shoes
        if(hasShoes)
        {
            dashSpeed = shoesDashSpeed;
            runSpeed = shoesRunSpeed;
            maxJumpcount = 2;
        }
        else
        {
            dashSpeed = originDashSpeed;
            runSpeed = originRunSpeed;
            maxJumpcount = 1;
        }


    }

    private void PosUpdate()
    {
        if (Inputx != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(Inputx), 1, 1);//Turn around the sprite. 
            if (Inputx > 0)
            {
                isTurnRight = true;
            }
            else
            {
                isTurnRight = false;
            }
        }

        //Change velocity
        if (!isUsingHook)
        {
            rb.velocity = new Vector2(Inputx * currentSpeed, rb.velocity.y);
        }
        //Here I choose to change the velocity directly.
        //Please notice that I changed the velocity directly instead of using force system. And there is a material
        //That erase all the friction on the wall to avoid bug that stick player to the wall, which means that
        //FORCE SYSTEM CAN'T BE USED TO CONTROL THE MOVEMENT IN THIS PROJECT. Otherwise everything is sliding.
        //Problem: When the character is too fast, he's easily crossing the wall.

        //Updated: I find a way to control the friction, and I found some problems when changing the velocity directly:
        //When the player moves too fast, the player may go across the wall. So considering the trade off, I choose to use force system instead.

        //AddForce
        //rb.AddForce(new Vector2(moveForce* Inputx, 0f),ForceMode2D.Impulse);
        /*if(Mathf.Abs(rb.velocity.x)>maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);//Get the real time speed and limit speed.
        }*/
        if (rb.velocity.y <= 0) //When Falling
        {
            if (Mathf.Abs(rb.velocity.y) > maxFallspeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxFallspeed);//Get the real time speed and limit speed.
            }
        }


    }

    private void DashCheck()
    {
        if (Inputx > 0 && !isRunning)
        {
            if (Time.time - rightPresstime <= maxWaittime)
            {
                randomc = Time.deltaTime - rightPresstime;
                canDash = true;
            }
            rightPresstime = Time.time;
        }

        else if (Inputx < 0 && !isRunning)
        {
            if (Time.time - leftPresstime <= maxWaittime)
            {
                canDash = true;
            }
            leftPresstime = Time.time;
        }

        if (MathF.Abs(movePress) == 1)
        {
            isRunning = true;
            if (canDash)
            {
                currentSpeed = dashSpeed;
                isDashing = true;
            }

            else
            {
                currentSpeed = runSpeed;
                isDashing = false;
            }
        }

        else
        {
            isRunning = false;
            isDashing = false;
            canDash = false;
            currentSpeed = runSpeed;
        }
    }

    private void originDashcheck()
    {
        if (MathF.Abs(Inputx) > 0)
        {
            isRunning = true;
            currentSpeed = runSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isDashing = true;
                currentSpeed = dashSpeed;
            }
            else
            {
                isDashing = false;
                currentSpeed = runSpeed;
            }
        }

    }

    private void CrouchUpdate()
    {
        //if(isOnGround()&&Input.GetButton)
    }

    private void linerDrageUpdate()
    {
        if ((Mathf.Abs(Inputx) < 0.5f) && isOnGround() && rb.velocity.x != 0)
        {
            //rb.AddForce(new Vector2( Mathf.Sign(rb.velocity.x) * (-groundFriction),0f));
            //rb.drag = groundFriction;
        }

        else if ((Mathf.Abs(Inputx) < 0.5f) && !isOnGround() && rb.velocity.x != 0)
        {
            //rb.drag = airFriction;
        }

        else
        {
            //rb.drag = 0;
        }
    }

    private void JumpUpdate()
    {
        if (!isUsingHook)
        {

            if(isOnGround()||isTouchingWall())
            {
                jumpCount = maxJumpcount;
            }
            //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);//One way for jumping. May change for optimization later
            //rb.velocity = Vector2.up * jumpSpeed;//When using this code, the camera slightly shakes when the player
            //drops to the ground. So I choose to use addforce for better experience.

            /*if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount--;
                audioSource.clip = jumpSound;
                audioSource.Play();
                isJumping = false;
            }*/

            if (isJumping)
            {
                //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);//One way for jumping. May change for optimization later
                //rb.velocity = Vector2.up * jumpSpeed;//When using this code, the camera slightly shakes when the player
                //drops to the ground. So I choose to use addforce for better experience.
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount--;
                audioSource.clip = jumpSound;
                audioSource.Play();
                isJumping = false;
            }


            //Hereis the code to optimize jumping
            if (rb.velocity.y <= 0) //When Falling
            {
                //The default gravity is 1.Thus here I need to decrease 1 to get the true gravity speed.
                //rb.velocity += Vector2.up* Physics2D.gravity.y*(fallAdd-1)*Time.fixedDeltaTime;
                rb.gravityScale = fallAdd;
            }
            else if (rb.velocity.y > 0 && !isHoldingJump)
            {
                //rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpAdd - 1) * Time.fixedDeltaTime;
                rb.gravityScale = jumpAdd;
            }
            else
            {
                rb.gravityScale = rbGrav;
            }
        }

        else
        {
            rb.gravityScale = rbGrav;
        }

        if (Mathf.Abs(rb.velocity.y) > maxFallspeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxFallspeed);//Get the real time speed.
        }

    }

    private void OnDrawGizmos()//You can also use Debug.DrawLine() function. In that case you have to run the game to seeGizmos lines, which is better for animation.
    {
        //Jump Refine
        Gizmos.DrawLine(transform.position + cornerRaycastPos, transform.position + cornerRaycastPos + Vector3.up * raycastLength);
        Gizmos.DrawLine(transform.position - cornerRaycastPos, transform.position - cornerRaycastPos + Vector3.up * raycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastPos, transform.position + innerRaycastPos + Vector3.up * raycastLength);
        Gizmos.DrawLine(transform.position - innerRaycastPos, transform.position - innerRaycastPos + Vector3.up * raycastLength);

        //Ground Tf
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundTf.position, new Vector3(0.3f, 0.6f, 0f));

        //Killpoint Tf
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(KillTf.position, 0.5f);
    }

    private void Raycastcollision()
    {
        isCorrecting = Physics2D.Raycast(transform.position + cornerRaycastPos, Vector2.up, raycastLength, groundMask) &&
            !Physics2D.Raycast(transform.position + innerRaycastPos, Vector2.up, raycastLength, groundMask) ||
            Physics2D.Raycast(transform.position - cornerRaycastPos, Vector2.up, raycastLength, groundMask) &&
            !Physics2D.Raycast(transform.position - innerRaycastPos, Vector2.up, raycastLength, groundMask)
            ;
    }

    private void CornerCorrect(float Yvelocity)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - innerRaycastPos + Vector3.up * raycastLength, Vector3.left, raycastLength, groundMask);
        //Cast the ray in unity and detect if it hits something.
        //Push the player to right if it detected something.
        if (hit.collider != null)
        {
            float newPos = hit.point.x - (transform.position.x - cornerRaycastPos.x);
            transform.position = new Vector3(transform.position.x + newPos, transform.position.y, 0);
            rb.velocity = new Vector2(rb.velocity.x, Yvelocity);
            return;
        }

        hit = Physics2D.Raycast(transform.position + innerRaycastPos + Vector3.up * raycastLength, Vector3.right, raycastLength, groundMask);
        if (hit.collider != null)
        {
            float newPos = -hit.point.x + (transform.position.x + cornerRaycastPos.x);
            transform.position = new Vector3(transform.position.x - newPos, transform.position.y, 0);
            rb.velocity = new Vector2(rb.velocity.x, Yvelocity);
            return;
        }
    }

    public bool isOnGround()
    {
        //Check if the player is on the ground by detecting distance of the ground point pos and ground.
        //return Physics2D.OverlapCircle(groundTf.position, 0.1f, groundMask)||Physics2D.OverlapCircle(groundTf.position, 0.1f,platformMask);
        return Physics2D.OverlapBox(groundTf.position,new Vector3(0.3f,0.6f,0f), 0f, groundMask) || Physics2D.OverlapBox(groundTf.position, new Vector3(0.3f, 0.6f, 0f), 0f, platformMask);
    }

    public bool isTouchingWall()
    {
        return Physics2D.OverlapCircle(leftWallTf.position, 0.1f, groundMask) || Physics2D.OverlapCircle(rightWallTf.position, 0.1f, platformMask);
    }

    private void EnemyCheck()
    {
       Collider2D enemy =Physics2D.OverlapCircle(KillTf.position,0.5f,enemyMask);
        if(enemy != null) 
        {
            Destroy(enemy.gameObject);

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    /*private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float dashGravity = rb.gravityScale;
        

        rb.AddForce(new Vector2(dashForce * Inputx, 0f),ForceMode2D.Impulse);

        

        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = dashGravity;
        isDashing = false;
        canDash = true;
    }*/
    //This is the dash code based on IEnumerator, which is not the effect I want. Maybe it's better to double press move button to dash.

    public void eraseJump()
    {
        jumpCount--;
    }

    public void EnableGrapple()
    {

        hasGrapple= true;
        hasShield= false;
        hasShoes= false;
        hasWatch= false;
    }
    public void EnableShield()
    {
        hasGrapple = false;
        hasShield = true;
        hasShoes = false;
        hasWatch = false;
    }

    public void EnableSoes()
    {
        hasGrapple = true;
        hasShield = false;
        hasShoes = true;
        hasWatch = false;

    }

}
