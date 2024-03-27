using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrapScript : MonoBehaviour
{
    PlayerLife PlayerLife;
    //Determines whether or not spikes are active
    bool willKill;
    //how long spikes are down
    [SerializeField] private float spikeTimeDown = 2;
    //how long spikes are up
    [SerializeField] private float spikeTimeUp = 2;
    //if false, spikes are down on scene start. If true, spikes are up on scene start
    [SerializeField] private float spikeOffset = 0;
    void Start()
    {
        PlayerLife = FindObjectOfType<PlayerLife>();
        StartCoroutine(SpikeTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpikeTimer()
    {
        yield return new WaitForSeconds(spikeOffset);
        while (true)
        {
            
            willKill = false;
            GetComponent<Renderer>().enabled = false; //remove when given proper animation
            yield return new WaitForSeconds(spikeTimeDown);
            willKill = true;
            GetComponent<Renderer>().enabled = true; //remove when given proper animation
            yield return new WaitForSeconds(spikeTimeUp);
            

            
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && willKill == true)
        {
            PlayerLife.PlayerDeath();
        }
    }
}
