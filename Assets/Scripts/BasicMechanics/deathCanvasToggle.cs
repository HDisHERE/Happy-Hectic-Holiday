using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvaHandlerScript : MonoBehaviour
{
    public Canvas deathCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (deathCanvas != null)
        {
            deathCanvas.enabled = false;
        }
    }

    public void ToggleCanvasOn(Canvas canvas)
    {
        if (deathCanvas != null) 
        {
            canvas.enabled = true;
        }
    }

    public void ToggleCanvasOff(Canvas canvas)
    {
        if (deathCanvas != null)
        {
            canvas.enabled = false;
        }
    }
}
