using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuSript : MonoBehaviour
{
    public string levelSelect1;
    public string levelSelect2;
    public string levelSelect3;
    public string mainMenu;
    public Canvas inGameMenu;
    private bool paused;

    //LevelEndScript levelEndScript;

    // Start is called before the first frame update
    void Start()
    {
        //levelEndScript = GetComponent<LevelEndScript>();
        inGameMenu.enabled = false;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (paused == false)
            {
                inGameMenu.enabled = true;
                paused = true;
            }
            else if (paused == true)
            {
                inGameMenu.enabled = false;
                paused = false;
            }


        }
    }

    public void Resume()
    {
        inGameMenu.enabled = false;
        paused = false;
    }

    public void LevelSelect()
    {
        if (LevelEndScript.level1Complete == false)
        {
            SceneManager.LoadScene(levelSelect1);
        }
        else if (LevelEndScript.level2Complete == false && LevelEndScript.level1Complete == true)
        {
            SceneManager.LoadScene(levelSelect2);
        }
        else if (LevelEndScript.level1Complete == true && LevelEndScript.level2Complete == true)
        {
            SceneManager.LoadScene(levelSelect3);
        }
    }

    public void MainMenu()
    {
        
        SceneManager.LoadScene(mainMenu);
        
    }
}
