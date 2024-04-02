using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlatform : MonoBehaviour
{
    private GameObject currentPlatform;

    private CapsuleCollider2D playerCollider;

    [SerializeField] private float coolDownTime;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if(currentPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
                

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            currentPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider=currentPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(platformCollider, playerCollider);

        yield return new WaitForSeconds(coolDownTime);

        Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
    }
}
