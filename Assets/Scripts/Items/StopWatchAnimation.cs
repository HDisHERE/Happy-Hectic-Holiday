using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatchAnimation : MonoBehaviour
{
    Animator ani;
    PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(!playerControl.isStopping)
        {
            ani.Play("stopwatchSleep");
        }
        else
        {
            ani.Play("stopwatchStop");
        }
    }
}
