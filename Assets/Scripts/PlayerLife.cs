using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator ani;

    public Transform playerSpawnPoint;

    CanvasHandlerScript canvasToggle;

    Rigidbody2D rb;



    //Determines whether player is dead or alive
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
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
            Respawn();
            Debug.Log("Respawn attempt");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            PlayerDeath();
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
