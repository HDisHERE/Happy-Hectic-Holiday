using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            //Invoke("Respawn", 1f);
            Respawn();
            Debug.Log("Respawn attempt");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "trap"|| collision.gameObject.tag == "enemy")
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerDeath()
    {
        canvasToggle.ToggleCanvasOn();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        dead = true;
    }
}
