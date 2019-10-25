using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public string sound;
    bool soundOn = true;
    bool musicOn = true;
    public GameManager gameManager;
    public GameObject xMarkerSound;
    public GameObject xMarkerMusic;

    private void OnMouseDown()
    {
        if (sound == "music")
        {
            if (musicOn)
            {
                gameManager.soundTrack.Pause();
                xMarkerMusic.SetActive(true);
                musicOn = false;
            }
            else if (!musicOn)
            {
                gameManager.soundTrack.UnPause();
                xMarkerMusic.SetActive(false);
                musicOn = true;
            }
        }
        else if (sound == "sound")
        {
            if (soundOn)
            {
                ChangeAudio(0);
                gameManager.soundTrack.volume = 0.2f;
                xMarkerSound.SetActive(true);
                soundOn = false;
            }
            else if (!soundOn)
            {
                ChangeAudio(1);
                gameManager.soundTrack.volume = 0.2f;
                xMarkerSound.SetActive(false);
                soundOn = true;
            }
        }
    }

    private void ChangeAudio(float volume)
    {
        AudioSource[] audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audio in audioSources)
        {
            audio.volume = volume;
        }
    }
}
