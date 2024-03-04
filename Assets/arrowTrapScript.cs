using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowTrapScript : MonoBehaviour
{
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnArrow());
    }

    IEnumerator SpawnArrow()
    {
        while(true)
        {
            Instantiate(arrow, transform.position, transform.rotation);
            yield return new WaitForSeconds(2f);
        }
    }
}
