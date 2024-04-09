using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    //The scene to be loaded
    public string sceneTitle;

    //loads the scene
    public void LoadScene(string sceneTitle)
    {
        SceneManager.LoadScene(sceneTitle);
        Debug.Log("scene " + sceneTitle + " load attempt");
    }

    // Restarts the scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        //Stop playing the scene if the application quits
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
