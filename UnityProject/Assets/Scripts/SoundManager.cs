using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;


    public AudioSource InGameAudio;
    public AudioSource PickUpAudio;

    private void Awake()
    {
        instance = this;

        InGameAudio.Stop();
        PickUpAudio.Stop();
    }

}
