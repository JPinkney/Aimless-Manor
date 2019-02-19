using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

    private List<Sound> soundsQueue = new List<Sound>();

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

    private void Update()
    {
        Sound firstSound = this.soundsQueue[0];
        if (firstSound == null)
        {
            return;
        }

        AudioSource firstSource = firstSound.source;
        if (!firstSource.isPlaying)
        {
            if (firstSound.hasBeenPlayed)
            {
                this.soundsQueue.RemoveAt(0);
            }
            else if(!firstSound.hasBeenPlayed)
            {
                firstSound.source.Play();
            }
        } else if (firstSource.isPlaying)
        {
            this.soundsQueue[0].hasBeenPlayed = true;
        }
    }

    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

        if (s.loop || s.isStackable)
        {
            s.source.Play();
        }
        else
        {
            this.soundsQueue.Add(s);
        }
	}

    public void Clear()
    {
        this.StopAll(this.soundsQueue);
    }

    public void WaitForCurrentlyPlayingSoundToFinish()
    {
        this.StopAll(soundsQueue.GetRange(1, soundsQueue.Count));
        this.soundsQueue.RemoveAll(sound => true);
    }

    private void StopAll(List<Sound> soundsToStop)
    {
        foreach(Sound s in soundsToStop)
        {
            s.source.Stop();
        }
    }

}
