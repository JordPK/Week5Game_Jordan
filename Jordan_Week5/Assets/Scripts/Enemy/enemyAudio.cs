using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAudio : MonoBehaviour
{
    AudioSource aud;

    public AudioClip footstep1;
    public AudioClip footstep2;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayFootstep1()
    {
        aud.PlayOneShot(footstep1, .2f);
    }

    void PlayFootstep2()
    {
        aud.PlayOneShot(footstep2, .2f);
    }
}
