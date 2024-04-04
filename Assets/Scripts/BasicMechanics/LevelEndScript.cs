using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndScript : MonoBehaviour
{
    public string LevelSelect = "LevelSelect";
    public string LevelSelect2 = "LevelSelect2";
    public string LevelSelect3 = "LevelSelect3";
    public static bool level1Complete = false;
    public static bool level2Complete = false;
    public int currentLevel;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player") && level1Complete == false)
        {
            if (this.currentLevel == 1)
            {
                level1Complete = true;
            }
            SceneManager.LoadScene(LevelSelect2);
            Debug.Log("load");
        }
        else if (collision.gameObject.CompareTag("Player") && level1Complete == true && level2Complete == false)
        {
            if (this.currentLevel == 2)
            {
                level2Complete = true;
            }
            SceneManager.LoadScene(LevelSelect3);
        }
        else if (collision.gameObject.CompareTag("Player") && level1Complete == true && level2Complete == true)
        {
            SceneManager.LoadScene(LevelSelect3);
        }

    }
}
