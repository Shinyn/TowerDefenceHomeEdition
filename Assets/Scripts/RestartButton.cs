using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
