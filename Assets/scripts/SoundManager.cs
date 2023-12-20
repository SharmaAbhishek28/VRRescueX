using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public AudioClip mySound;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audio.PlayOneShot(mySound);
    }

    public void StopSound()
    {
        audio.Stop();
    }
}
