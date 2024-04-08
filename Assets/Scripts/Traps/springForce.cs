using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springForce : MonoBehaviour
{
    [SerializeField] private float Force=2000f;

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
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*Force, ForceMode2D.Impulse);

            collision.gameObject.GetComponent<PlayerControl>().jumpCount = 1;
        }
    }
}
