using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandlerScript : MonoBehaviour
{
    public Canvas deathCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if(deathCanvas != null)
        {
            deathCanvas.enabled = false;
        }
    }

    public void ToggleCanvasOn()
    {
        if(deathCanvas != null) 
        {
            deathCanvas.enabled = true;
        }
    }

    public void ToggleCanvasOff()
    {
        if (deathCanvas != null)
        {
            deathCanvas.enabled = false;
        }
    }
}
