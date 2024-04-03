using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBG : MonoBehaviour
{
    public AudioSource musicBG, loopMusic;
    // Start is called before the first frame update
    void Start()
    {
        musicBG.Play();
        loopMusic.PlayScheduled(AudioSettings.dspTime + musicBG.clip.length);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
