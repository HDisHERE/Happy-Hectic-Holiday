using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformScript : MonoBehaviour
{
    public float delay = 1.5f;
    [SerializeField] private bool istouched = false;
    public float fadeDuration = 1.0f;
    public float reappearDelay = 3f;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public bool reset = false;
    public canvaHandlerScript deathReset;
    Color StartColour;
    Color EndColour;

    StopTime stopTime;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        deathReset = FindObjectOfType<canvaHandlerScript>();
        stopTime = GetComponent<StopTime>();

        StartColour = spriteRenderer.color;
        EndColour = new Color(StartColour.r, StartColour.g, StartColour.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(deathReset.enabled)
        {
            reset = true;
        }*/

        /*if(reset)
        {
            gameObject.SetActive(true);
            reset=false;
        }*/

        if (stopTime.isStoped)
        {
            StopAllCoroutines();
        }

        



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && istouched == false)
        {
            istouched = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(fadeDuration);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float fadeTime = 0f;

        yield return new WaitForSeconds(delay);
        while (fadeTime < delay)
        {
            float t = fadeTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(StartColour, EndColour, t);
            fadeTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = EndColour;
        boxCollider.enabled = false;

        StartCoroutine(Reappear());
    }

    IEnumerator Reappear()
    {
        yield return new WaitForSeconds(reappearDelay);
        spriteRenderer.color = StartColour;
        boxCollider.enabled = true;
        istouched = false;
    }
}
