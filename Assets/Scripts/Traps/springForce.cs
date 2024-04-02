using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springForce : MonoBehaviour
{
    [SerializeField] private float Force=20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,Force),ForceMode2D.Impulse);

            collision.gameObject.GetComponent<PlayerControl>().eraseJump();
        }
    }
}
