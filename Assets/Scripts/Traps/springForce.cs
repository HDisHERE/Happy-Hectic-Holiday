using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springForce : MonoBehaviour
{
    [SerializeField] private float Force=20f;

    [SerializeField] private bool isOnGround;

    private float originForce;

    StopTime stopTime;
    // Start is called before the first frame update
    void Start()
    {
        stopTime=GetComponent<StopTime>();
        originForce = Force;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopTime.isStoped)
        {
            Force = 0f;
        }
        else
        {
            Force = originForce;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, Force), ForceMode2D.Impulse);

            if(isOnGround)
            {
                collision.gameObject.GetComponent<PlayerControl>().eraseJump();
            }
            
        }
    }
}
