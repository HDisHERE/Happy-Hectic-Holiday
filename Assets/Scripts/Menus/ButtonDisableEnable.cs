using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisableEnable : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;

    // What happens when the level select button is clicked
    public void levelSelectClicked()
    {
        // If level 1 button and level 2 buttons are disabled in the hierarchy, then enable them
        if(level1.activeInHierarchy == false || level2.activeInHierarchy == false)
        {
            level1.SetActive(true);
            level2.SetActive(true);
        }
        // If level 1 button and level 2 buttons are already enabled in the hierarchy, then disable them
        else
        {
            level1.SetActive(false);
            level2.SetActive(false);
        }
    }
}
