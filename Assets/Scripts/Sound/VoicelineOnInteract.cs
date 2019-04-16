using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicelineOnInteract : Interactable
{

    private bool hasPlayed = false;
    private bool hasPlayed2 = false;

    //For the voicelines
    public AudioClip voicelineNoise;
    private AudioSource voicelineSource;
    public AudioClip voiceLine;
    private AudioSource voiceSource;

    private AudioManager audioManager;

    public void Start()
    {
        audioManager = AudioManager.instance;
    }

    public override void Interact(Inventory inv, GameObject obj)
    {
        if (voiceLine && hasPlayed == false)
        {
            audioManager.Play(voicelineNoise);
            hasPlayed = true;
        }

        if (voicelineNoise && hasPlayed2 == false)
        {
            audioManager.Play(voicelineNoise);
            hasPlayed2 = true;
        }

    }

}
