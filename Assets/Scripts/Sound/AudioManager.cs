using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private AudioSource source;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /*
     * This just sets the source to be whatever source you passed in
     */
    public void Play(AudioSource sound)
    {
        if (source != null)
        {
            source.Stop();
            SetSource(sound);
        }

    }

    /*
     * This one sets the narration to be on the player instead of on whatever
     * object you're making the noise on so you don't need to provide the source
     */
    public void Play(AudioClip sound)
    {
        if (source != null)
        {
            source.Stop();
            SetSource(sound);
        }
       
    }

    private void SetSource(AudioSource sound)
    {
        source = sound;
        source.Play();
    }

    private void SetSource(AudioClip sound)
    {
        if (sound != null)
        {
            source.clip = sound;
            source.Play();
        }
    }

}