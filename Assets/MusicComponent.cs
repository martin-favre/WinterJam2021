using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicComponent : MonoBehaviour
{
    // Thanks http://www.internetaudioguy.com/iag/freemusic/freemusic.htm
    // Sound from Zapsplat.com
    AudioSource source;
    static MusicComponent instance;
    void Start()
    {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

}
