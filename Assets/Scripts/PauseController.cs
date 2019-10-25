using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    public string name;
    string pauseButton = "pauseButton";
    string unpauseButton = "unpausButton";
    public GameObject pauseBtn;

    private void OnMouseDown()
    {
        if (name == pauseButton)
        {
            ToggleTime();
            pauseMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (name == unpauseButton)
        {
            ToggleTime();
            pauseMenu.gameObject.SetActive(false);
            pauseBtn.SetActive(true);
        }
    }

    public void ToggleTime()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
