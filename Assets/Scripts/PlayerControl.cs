using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Basic Data
    //Walk
    Rigidbody2D rb;
    public float moveSpeed = 16f;
    [Header("Player Move:")]
    //public float dashForce = 2.5f;
    private float Inputx;

    //With Force system
    //public float moveForce;
    //public float maxSpeed;
    //public float groundFriction;


    //Jump
    [Header("Player Jump:")]
    [Range(0f, 50f)]
    //public float jumpSpeed = 30.0f;
    public float jumpForce = 40.0f;
    private bool isJumping=false;
    private float rbGrav;
    public float maxFallspeed;

    //Better Jump
    public float fallAdd;
    public float jumpAdd;
    public float airFriction;
    private bool isHoldingJump=false;

    //Dash
    [Header("Player Dash:")]
    private bool canDash=true;
    private bool isDashing=false;
    public float dashForce = 20.0f;
    public float dashTime = 0.2f;
    public float dashMaxspeed = 40f;

    //Death
    public Transform playerSpawnPoint;

    CanvasHandlerScript canvasToggle;
    public GameObject GunPivot;
    public GameObject Shield;

    public static bool hasGrapple;
    public static bool hasShield;

    //Determines whether player is dead or alive
    bool dead;

    private Transform groundTf;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rbGrav = rb.gravityScale;
        groundTf=transform.Find("Ground");
        canvasToggle=GetComponentInChildren<CanvasHandlerScript>();
        if(hasShield)
        {
            Shield.SetActive(true);
        }
        if(hasGrapple)
        {
            GunPivot.SetActive(true);
        }

        Respawn();
    }

    // Update is called once per frame
    void Update()
    {

        //Here is everything about input.
        if (!dead)
        {
            getInput();
            if (isOnGround() && isJumping)
            {
                //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);//One way for jumping. May change for optimization later
                //rb.velocity = Vector2.up * jumpSpeed;//When using this code, the camera slightly shakes when the player
                //drops to the ground. So I choose to use addforce for better experience.
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && dead == true)
        {
            Respawn();
            Debug.Log("Respawn attempt");
        }

        //Shift to dash
        if(Input.GetKey(KeyCode.LeftShift)&&canDash)
        {
            StartCoroutine(Dash());
        }
    }
        //if the player is dead, respawn them when they press space
        
    private void getInput()
    {
        Inputx = Input.GetAxis("Horizontal");//-1~1//Get input every frame.
        //Inputx = Input.GetAxisRaw("Horizontal");//-1,0,1//
        isJumping = Input.GetButtonDown("Jump");
        isHoldingJump = Input.GetButton("Jump");
    }
    
    private void FixedUpdate()
    {
        //Here is everything about physical calculation.
        if (!dead)
        {
            PosUpdate();
            JumpUpdate();
            //linerDrageUpdate();//Works with force system
        }

    }

    private void PosUpdate()
    {
        
        if (Inputx != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(Inputx), 1, 1);//Turn around the sprite. 
        }

        //Chnage velocity
        rb.velocity = new Vector2(Inputx * moveSpeed, rb.velocity.y);
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
    }


    private void linerDrageUpdate()
    {
        if((Mathf.Abs(Inputx)<0.5f)&&isOnGround()&&rb.velocity.x!=0)
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
            rb.gravityScale = rbGrav;
        }

        if (Mathf.Abs(rb.velocity.y) > maxFallspeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxFallspeed);//Get the real time speed.
        }
    }

    public bool isOnGround()
    {
        //Check if the player is on the ground by detecting distance of the ground point pos and ground.
        return Physics2D.OverlapCircle(groundTf.position, 0.05f, LayerMask.GetMask("ground"));
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float dashGravity = rb.gravityScale;
        

        rb.AddForce(new Vector2(dashForce * Inputx, 0f),ForceMode2D.Impulse);

        

        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = dashGravity;
        isDashing = false;
        canDash = true;
    }

    public void EnableGrapple()
    {
        hasGrapple= true;
        hasShield= false;
    }
    public void EnableShield()
    {
        hasShield= true;
        hasGrapple= false;
    }
}
