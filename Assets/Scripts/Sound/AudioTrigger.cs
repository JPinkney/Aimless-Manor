using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public bool playOnce = true;
    private bool hasPlayed = false;
    public AudioClip someAudio;
    private AudioSource someSource;

    private void Start()
    {
        someSource = gameObject.AddComponent<AudioSource>();
        someSource.clip = someAudio;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && ((!playOnce) || (playOnce && !hasPlayed)))
        {
            someSource.Play();
            hasPlayed = true;
        }
    }
}
