using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringSoundScript : MonoBehaviour
{
    [Header("Audio Resources")]
    public AudioClip SpringBounce;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SpringBounce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            audioSource.Play();
        }
    }
}
