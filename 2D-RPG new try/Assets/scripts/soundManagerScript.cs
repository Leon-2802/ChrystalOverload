using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerScript : MonoBehaviour
{

    public AudioClip laserGun;
    public AudioSource audioSrc;

    public void Start()
    {
        // laserGun = Resources.Load<AudioClip>("sfx/344276__nsstudios__laser3.wav");
        audioSrc = GetComponent<AudioSource>();
    }


    void Update()
    {
        
    }

    public void PlaySound (string clip)
    {
        switch (clip) 
        {
            case "laser":
                audioSrc.PlayOneShot (laserGun);
                break;
        }
    }
}
