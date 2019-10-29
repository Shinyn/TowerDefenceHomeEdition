using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject quitButton;

    string mainScene = "Main";

    private void OnMouseDown()
    {
        if (restartButton)
        {
            Debug.Log("Restart");
            SceneManager.LoadScene(mainScene);
        }

        if (quitButton)
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
}
