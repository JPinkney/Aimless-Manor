using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicRoomInitializer : MonoBehaviour
{

    public AudioMixer mixer;
    public AudioMixerSnapshot[] roomSnapshots;
    public float[] weights;
    public float fadeIn = 0.3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            mixer.TransitionToSnapshots(roomSnapshots, weights, fadeIn);
        }

    }
}
