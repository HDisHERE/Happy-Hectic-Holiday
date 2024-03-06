using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrapScript : MonoBehaviour
{
    PlayerControl playerControl;
    //Determines whether or not spikes are active
    bool willKill;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        StartCoroutine(SpikeTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpikeTimer()
    {
        while (true)
        {
            willKill= false;
            GetComponent<Renderer>().enabled = false; //remove when given proper animation
            yield return new WaitForSeconds(2f);
            willKill= true;
            GetComponent<Renderer>().enabled = true; //remove when given proper animation
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && willKill == true)
        {
            playerControl.PlayerDeath();
        }
    }
}
