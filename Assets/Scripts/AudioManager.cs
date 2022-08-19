using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] _sounds;

    private void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = false;
        }
    }

    public void Play(string clipName)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == clipName);

        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found!");
        }

        s.source.Play();
    }
}