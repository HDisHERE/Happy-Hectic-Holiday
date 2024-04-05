using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    PlayerControl playerControl;

    private Animator ani;

    public static Transform playerSpawnPoint;

    CanvasHandlerScript canvasToggle;

    Rigidbody2D rb;

    private int currentSceneIndex;

    public GameObject objectToPreserve;

    //Determines whether player is dead or alive
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canvasToggle = GetComponentInChildren<CanvasHandlerScript>();
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dead == true)
        {
            //Invoke("Respawn", 1f);
            Respawn();
            Debug.Log("Respawn attempt");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!playerControl.isCrashing)
        {
            if (collision.gameObject.tag == "trap" ||
                (collision.gameObject.tag == "enemy"&& !collision.gameObject.GetComponent<EnemyMoving>().isStun)
                &&!playerControl.isStopping)
            {
                PlayerDeath();
            }
        }
        
    }

    public void Respawn()
    {
        canvasToggle.ToggleCanvasOff();
        if (playerSpawnPoint != null)
        {
            rb.isKinematic = false;
            dead = false;
            transform.position = playerSpawnPoint.position;
        }
        
    }

    public void PlayerDeath()
    {
        canvasToggle.ToggleCanvasOn();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        dead = true;
    }
}
