using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTime : MonoBehaviour
{
    public bool isStoped = false;
    private float stopTime=2f;
    private GameObject player;
    private PlayerControl playerControl;
    private Vector2 velocityBeforeStop;

    Rigidbody2D rb;

    RigidbodyType2D originType;

    // Start is called before the first frame update
    void Start()
    {
        isStoped = false;
        rb=GetComponent<Rigidbody2D>();
        originType = rb.bodyType;
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStoped&&playerControl.isStopping)
        {
            velocityBeforeStop = rb.velocity;
            StopAllCoroutines();
            
            StartCoroutine(Stop());
        }
    }

    public Vector2 GetVelocityBeforeStop()
    {
        return velocityBeforeStop;
    }

    IEnumerator Stop()
    {
        isStoped = true;

        rb.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(stopTime);

        rb.bodyType = originType;

        rb.AddForce(velocityBeforeStop.normalized*5f,ForceMode2D.Impulse);

        isStoped =false;
    }
}
