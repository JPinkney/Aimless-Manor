using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicelineOnActive : MonoBehaviour
{
    public bool playOnce = true;
    private bool hasPlayed = false;
    private bool activity = false;
    public GameObject LineObject;
    public AudioClip someAudio;
    private AudioSource someSource;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        someSource = gameObject.AddComponent<AudioSource>();
        someSource.clip = someAudio;
    }

    // Update is called once per frame
    void Update()
    {
        if (LineObject.activeInHierarchy == true)
        {
            activity = true;
        }

        if (someAudio && activity == true && hasPlayed == false)
        {
            audioManager.Play(someSource);
            hasPlayed = true;
        }
    }
}
