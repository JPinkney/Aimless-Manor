using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public AudioClip keyDropNoise;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("key"))
        {
            AudioSource keySource = gameObject.AddComponent<AudioSource>();
            keySource.clip = keyDropNoise;
            keySource.Play();
        }
    }
}
