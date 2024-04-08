using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowTrapScript : MonoBehaviour
{
    public GameObject arrow;
    [SerializeField] private float arrowCooldown = 2;
    [SerializeField] private float arrowOffset = 0;
    public bool verticalArrow = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnArrow());
    }

    private void Update()
    {
        //StartCoroutine(SpawnArrow());
    }

    IEnumerator SpawnArrow()
    {
        
        yield return new WaitForSeconds(arrowOffset);
        while(true)
        {
           
            if(verticalArrow)
            {
                arrow.GetComponent<arrowScript>().isVertical = true;
                Instantiate(arrow, transform.position, transform.rotation);
            }
            else
            {
                arrow.GetComponent<arrowScript>().isVertical = false;
                Instantiate(arrow, transform.position, transform.rotation);
            }
            
            yield return new WaitForSeconds(arrowCooldown);
        
        }
        
    }
}
