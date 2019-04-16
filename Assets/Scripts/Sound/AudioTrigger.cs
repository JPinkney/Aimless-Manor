using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public bool playOnce = true;
    private bool hasPlayed = false;
    public AudioClip someAudio;
    private AudioSource someSource;
    private AudioManager audioManager;

    public ParticleSystem confettiRain;

    private void Start()
    {
        audioManager = AudioManager.instance;
        someSource = gameObject.AddComponent<AudioSource>();
        someSource.clip = someAudio;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && ((!playOnce) || (playOnce && !hasPlayed)))
        {
            audioManager.Play(someSource);
            hasPlayed = true;

            if (confettiRain)
            {
                confettiRain.Play();
            }

        }

    }
}
