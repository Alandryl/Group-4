using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioSource audioSource;

    [Header("Audio")]
    public AudioClip audioFootstep;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void Footstep()
    {
        audioSource.PlayOneShot(audioFootstep);
    }
}
