using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    //This is something similar to Fading platform.
    [SerializeField] private float fallDelay=1.5f;
    [SerializeField] private float reappearDelay=3f;
    private bool touched = false;
    //[SerializeField] private float destroyDelay = 1.5f;

    Vector2 startPosition;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player" && touched == false)
        {
            touched = true;
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        //Destroy(gameObject,destroyDelay);
        yield return new WaitForSeconds(reappearDelay);
        rb.bodyType = RigidbodyType2D.Kinematic;
        gameObject.transform.position = startPosition;
        touched = false;
    }
}
