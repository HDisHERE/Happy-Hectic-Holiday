using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Points;
    
    private int pointNum=1;
    
    
    [SerializeField] private float Speed = 2f;
    public float wait;

    //private Vector2 startPos;
    //private Vector2 destPos;
    private float direction = 1f;
    private bool isWaiting = false;

    StopTime stopTime;

    void Start()
    {
        stopTime = GetComponent<StopTime>();
        StartCoroutine(WaitAtDestination());
    }

    void Update()
    {
        MovebyPoint();
    }

    private void MovebyPoint()
    {
        if(!stopTime.isStoped)
        {
            if (!isWaiting)
            {
                transform.position = Vector2.MoveTowards(transform.position, Points[pointNum].transform.position, Speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, Points[pointNum].transform.position) < 0.1f)
                {
                    StartCoroutine(WaitAtDestination());
                }
            }
        }
        
    }

    /*private void MovebyDis()
    {
        if (!isWaiting)
        {
            transform.position = Vector2.MoveTowards(transform.position, destPos, Speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destPos) < 0.1f)
            {
                StartCoroutine(WaitAtDestination());
            }
        }
    }*/

    IEnumerator WaitAtDestination()
    {
        pointNum++;
        if(pointNum>=Points.Length)
        {
            pointNum = 0;
        }
        
        isWaiting = true;
        yield return new WaitForSeconds(wait);
        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    /*private void CalculateDestination()
    {
        destPos = startPos + Vector2.right * distance * direction;
        direction *= -1;
    }*/
}
