using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public string sound;
    bool soundOn = true;
    bool musicOn = true;
    public GameManager gameManager;

    private void OnMouseDown()
    {
        if (sound == "music")
        {
            if (musicOn)
            {
                gameManager.soundTrack.Pause();
                musicOn = false;
            }
            else if (!musicOn)
            {
                gameManager.soundTrack.UnPause();
                musicOn = true;
            }
        }
        else if (sound == "sound")
        {
            if (soundOn)
            {
                AudioListener.volume = 0f;
                soundOn = false;
            }
            else if (!soundOn)
            {
                AudioListener.volume = 1f;
                soundOn = true;
            }
        }
    }
}
