using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrapAltScript : MonoBehaviour
{
    //Determines whether or not spikes are active
    bool willKill;
    // Start is called before the first frame update
    void Start()
    {
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
            willKill = true;
            yield return new WaitForSeconds(2f);
            willKill = false;
            yield return new WaitForSeconds(2f);
        }
    }
}
