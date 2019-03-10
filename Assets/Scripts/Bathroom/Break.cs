﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : Interactable
{
    public Transform brokenGlass;
    public ParticleSystem glassShatter;

    //For the breaking noise
    public AudioClip breakNoise;
    private AudioSource breakSource;

    //For the voicelines
    public AudioClip voicelineNoise;
    private AudioSource voicelineSource;

    public Transform newKeyLocation;
    public GameObject keyObj;
    public ParticleSystem glimmer;

    private AudioManager audioManager;

    public void Start()
    {
        audioManager = AudioManager.instance;
    }

    public override void Interact(Inventory inv, GameObject obj)
    {
        if (breakNoise)
        {
            AudioSource.PlayClipAtPoint(breakNoise, this.transform.position);
        }

        if (voicelineNoise)
        {
            audioManager.Play(voicelineNoise);
        }
       
        this.gameObject.SetActive(false);

        if (brokenGlass)
        {
            Instantiate(brokenGlass, transform.position, transform.rotation);
            brokenGlass.localScale = transform.localScale;
        }

        if (glassShatter)
        {
            glassShatter.Play();
        }

        if (keyObj)
        {
            keyObj.transform.position = newKeyLocation.position;
        }

        if (glimmer)
        {
            glimmer.Play();
        }

        Physics.IgnoreLayerCollision(9, 9);
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

    }
}
