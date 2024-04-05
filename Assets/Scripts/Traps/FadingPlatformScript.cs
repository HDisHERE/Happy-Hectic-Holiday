using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformScript : MonoBehaviour
{
    public float delay = 1.5f;
    private bool touched = false;
    public float fadeDuration = 1.0f;
    public float reappearDelay = 3f;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public bool reset = false;
    public canvaHandlerScript deathReset;

    StopTime stopTime;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        deathReset = FindObjectOfType<canvaHandlerScript>();
        stopTime=GetComponent<StopTime>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(deathReset.enabled)
        {
            reset = true;
        }

        if(reset)
        {
            gameObject.SetActive(true);
            reset=false;
        }

        if(stopTime.isStoped)
        {
            StopAllCoroutines();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !touched)
        {
            touched = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade() 
    {
        float fadeTime = 0f;
        Color startColour = spriteRenderer.color;
        Color EndColour = new Color(startColour.r, startColour.g, startColour.b, 0f);
        while (fadeTime < delay)
        {
            float t = fadeTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(startColour, EndColour, t);
            fadeTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = EndColour;
        
        //yield return new WaitForSeconds(delay);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(reappearDelay);
        spriteRenderer.color = startColour;
        boxCollider.enabled = true;
        touched= false;
    }
}
