using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class soundManagerScript : MonoBehaviour
{

    public sounds[] sound;

    void Awake()
    {
        foreach (sounds s in sound)  
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    public void Play (string name)
    {
        sounds s = Array.Find(sound, sounds => sounds.name == name);
        s.source.Play();
    }
    public void Stop (string name)
    {
        sounds s = Array.Find(sound, sounds => sounds.name == name);
        s.source.Stop();
    }
}
