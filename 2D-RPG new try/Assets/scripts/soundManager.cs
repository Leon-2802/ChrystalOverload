using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip shot;
    public AudioClip coin;
    public AudioClip collectItem;
    public AudioClip emptyMag;
    public AudioClip reload;
    public AudioClip enemyDeath;
    public AudioClip error;
    public AudioClip smallTurret;
    public AudioClip bigTurret;
    
    public static soundManager sManagerInstance; //gibt immer nur eine einzige Instanz, daher die Abfrage in Awake
    private void Awake() 
    {
        if(sManagerInstance != null && sManagerInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sManagerInstance = this;
        DontDestroyOnLoad(this);
    }
}
