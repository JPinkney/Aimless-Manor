﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBreak : Interactable
{
    public Transform brokenGlass;
    public ParticleSystem glassShatter;

    //For the breaking noise
    public AudioClip breakNoise;
    private AudioSource breakSource;

    //For the voicelines
    public AudioClip voicelineNoise;
    private AudioSource voicelineSource;

    public bool kitPuzzle;
    public GameObject kitMan;

    private Puzzle kitScript;

    //public Transform newKeyLocation;
    //public GameObject keyObj;
    //public ParticleSystem glimmer;

    private AudioManager audioManager;

    public void Start()
    {
        audioManager = AudioManager.instance;
        if (kitPuzzle)
        {
            kitScript = kitMan.GetComponent<Puzzle>();
            
        }
        else
        {
            kitMan = null;
            kitScript = null;
        }
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

        // Instantiate(brokenGlass, transform.position, transform.rotation);
        // brokenGlass.localScale = transform.localScale;
        // highlightLight.SetActive(false);

        if (brokenGlass)
        {
            Instantiate(brokenGlass, transform.position, transform.rotation);
            brokenGlass.localScale = transform.localScale;
        }

        if (glassShatter)
        {
            glassShatter.Play();
        }

        if (kitPuzzle)
        {
            kitScript.PlateBroke();
        }

        Physics.IgnoreLayerCollision(9, 9);
        //keyObj.transform.position = newKeyLocation.position;
        //glimmer.Play();

        //Physics.IgnoreLayerCollision(9, 9);
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

    }

}
