using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public Transform brokenGlass;
    public ParticleSystem glassShatter;

    public AudioClip breakNoise;
    private AudioSource breakSource;

    void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(breakNoise, this.transform.position);

        //Destroy(gameObject);
        this.gameObject.SetActive(false);
        Instantiate(brokenGlass, transform.position, transform.rotation);
        brokenGlass.localScale = transform.localScale;

        glassShatter.Play();

    }

}
