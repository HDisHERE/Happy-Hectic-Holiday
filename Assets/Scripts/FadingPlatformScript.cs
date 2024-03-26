using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformScript : MonoBehaviour
{
    public float delay = 1.5f;
    private bool touched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !touched)
        {
            touched = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade() 
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
