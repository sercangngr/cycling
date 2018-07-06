using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    public static Sound instance;


    public AudioSource InGameAudio;


    private void Awake()
    {
        instance = this;

        InGameAudio.Stop();
        
    }
}
