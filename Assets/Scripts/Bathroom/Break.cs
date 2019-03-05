using System.Collections;
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
    public GameObject glimmer;

    public override void Interact(Inventory inv, GameObject obj)
    {
        AudioSource.PlayClipAtPoint(breakNoise, this.transform.position);
        AudioSource.PlayClipAtPoint(voicelineNoise, this.transform.position);
        //Destroy(gameObject);
        this.gameObject.SetActive(false);
        Instantiate(brokenGlass, transform.position, transform.rotation);
        brokenGlass.localScale = transform.localScale;

        glassShatter.Play();
        keyObj.transform.position = newKeyLocation.position;
        glimmer.SetActive(true);
    }
}
