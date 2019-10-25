using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject exitButton;
    public GameObject MENU;
    AudioSource menuSoundtrack;

    void Start()
    {
        menuSoundtrack = GetComponent<AudioSource>();
        Time.timeScale = 0;
    }

    private void OnMouseDown()
    {
        if (playButton)
        {
            menuSoundtrack.Stop();
            gameManager.soundTrack.Play();
            Time.timeScale = 1;
            MENU.gameObject.SetActive(false);
        }
        else if (creditsButton)
        {
            // visa credits i guess
            Debug.Log("Credits");
        }
        else if (exitButton)
        {
            Debug.Log("AppQuit");
            Application.Quit();
        }
    }

}
