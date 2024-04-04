using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    //This is something similar to Fading platform.
    [SerializeField] private float fallDelay=1.5f;
    [SerializeField] private float destroyDelay = 1.5f;

    Rigidbody2D rb;
    StopTime stopTime;
    // Start is called before the first frame update
    void Start()
    {
        stopTime=GetComponent<StopTime>();
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopTime.isStoped)
        {
            StopAllCoroutines();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(Fall());
            }
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(DestoryPlatform());
    }

    private IEnumerator DestoryPlatform()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
