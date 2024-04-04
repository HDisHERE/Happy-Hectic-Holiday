using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] Points;

    private GameObject player;

    private int pointNum = 0;

    Rigidbody2D rb;


    [SerializeField] private float Speed = 2f;
    [SerializeField] private float waitTime;

    //private Vector2 startPos;
    //private Vector2 destPos;
    private float direction = 1f;
    private bool isWaiting = false;

    public bool isStun = false;
    [SerializeField] private float stunTime;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MovebyPoint();
        if (player.GetComponent<PlayerControl>().isCrashing)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void FixedUpdate()
    {
        transform.localScale = new Vector3(Mathf.Sign(faceTowards()), 1, 1);
    }

    private float faceTowards()
    {
        return (Points[pointNum].transform.position - transform.position).normalized.x; 
    }

    private void MovebyPoint()
    {
        if (!isWaiting)
        {
            if (!isStun)
            {
                transform.position = Vector2.MoveTowards(transform.position, Points[pointNum].transform.position, Speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, Points[pointNum].transform.position) < 0.5f)
                {
                    pointNum++;

                    if (pointNum >= Points.Length)
                    {
                        pointNum = 0;
                    }

                    StartCoroutine(WaitAtDestination());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("shield")&&!isStun&&player.GetComponent<PlayerControl>().isCrashing)
        {
            StartCoroutine(Stun());
        }
    }


    IEnumerator WaitAtDestination()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }

    IEnumerator Stun()
    {
        isStun = true;

        rb.gravityScale = 1.0f;

        rb.velocity = new Vector2(rb.velocity.x, 7f);

        yield return new WaitForSeconds(stunTime);

        rb.gravityScale = 0f;

        rb.velocity = new Vector2(0f, 0f);

        isStun =false;
    }

    IEnumerator DisableCollision()
    {
        CapsuleCollider2D enemyCollider=GetComponent<CapsuleCollider2D>();

        CapsuleCollider2D playerCollider=player.GetComponent<CapsuleCollider2D>();

        Physics2D.IgnoreCollision(enemyCollider, playerCollider);

        yield return new WaitForSeconds(0.2f);

        Physics2D.IgnoreCollision(enemyCollider, playerCollider,false);
    }
}
