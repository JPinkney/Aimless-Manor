using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronItemDrop : MonoBehaviour
{
   
    public ParticleSystem waterEffect;
    private bool showWaterEffect;
    public AudioClip dropNoise;
    private AudioSource dropSound;
    private bool soundIsPlaying = false;

    private void Start()
    {
        dropSound = gameObject.AddComponent<AudioSource>();
        dropSound.clip = dropNoise;
    }

    private void OnTriggerEnter(Collider other)
    {
        this.showWaterEffect = true;
    }

    IEnumerator ExecuteItemDropEffect(float time)
    {
        this.waterEffect.Play();

        //Play the audio with it
        if (!soundIsPlaying)
        {
            dropSound.Play();
            soundIsPlaying = true;
        }

        yield return new WaitForSeconds(time);
        this.showWaterEffect = false;
        this.waterEffect.Stop();
        dropSound.Stop();
        soundIsPlaying = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (this.showWaterEffect)
        {
            StartCoroutine(this.ExecuteItemDropEffect(1));
        }
    }
}
