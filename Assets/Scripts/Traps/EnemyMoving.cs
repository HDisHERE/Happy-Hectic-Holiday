using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] Points;

    private int pointNum = 1;


    [SerializeField] private float Speed = 2f;
    [SerializeField] private float wait;

    //private Vector2 startPos;
    //private Vector2 destPos;
    private float direction = 1f;
    private bool isWaiting = false;

    void Start()
    {

    }

    void Update()
    {
        MovebyPoint();
    }

    private void MovebyPoint()
    {
        if (!isWaiting)
        {
            transform.position = Vector2.MoveTowards(transform.position, Points[pointNum].transform.position, Speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, Points[pointNum].transform.position) < 0.1f)
            {
                pointNum++;

                if (pointNum >= Points.Length)
                {
                    pointNum = 0;
                }

                StartCoroutine(WaitAtDestination());
            }
        }
    }

    IEnumerator WaitAtDestination()
    {
        isWaiting = true;
        yield return new WaitForSeconds(wait);
        isWaiting = false;
    }
}
