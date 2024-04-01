using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformScript : MonoBehaviour
{
    public float delay = 1.5f;
    private bool touched = false;
    public float fadeDuration = 1.0f;
    private SpriteRenderer spriteRenderer;
    public bool reset = false;
    public canvaHandlerScript deathReset;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        deathReset = FindObjectOfType<canvaHandlerScript>();
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
        float fadeTime = 0f;
        Color startColor = spriteRenderer.color;
        Color EndColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        while (fadeTime < delay)
        {
            float t = fadeTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(startColor, EndColor, t);
            fadeTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the target color is set
        spriteRenderer.color = EndColor;

        //yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
