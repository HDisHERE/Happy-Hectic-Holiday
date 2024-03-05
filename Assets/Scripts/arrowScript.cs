using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    Rigidbody2D rigidBody;

    //speed of the arrow
    float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        //moves the arrow
        rigidBody.velocity = transform.up*moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
