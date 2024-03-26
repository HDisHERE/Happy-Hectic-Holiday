using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public float distance;
    public float speed;
    public float wait;

    private Vector2 startPos;
    private Vector2 destPos;
    private float direction = 1f;
    private bool isWaiting = false;

    void Start()
    {
        startPos = transform.position;
        CalculateDestination();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isWaiting)
        {
            transform.position = Vector2.MoveTowards(transform.position, destPos, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destPos) < 0.1f)
            {
                StartCoroutine(WaitAtDestination());
            }
        }
    }

    IEnumerator WaitAtDestination()
    {
        isWaiting = true;
        yield return new WaitForSeconds(wait);
        CalculateDestination();
        isWaiting = false;
    }

    private void CalculateDestination()
    {
        destPos = startPos + Vector2.right * distance * direction;
        direction *= -1;
    }
}
