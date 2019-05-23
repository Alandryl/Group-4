using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    [Range(0f, 1f)]
    public float audioVolume = 1;

    public AudioClip currentTrack;

    public bool fadeMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = audioVolume;
        audioSource.clip = currentTrack;
        audioSource.Play();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (fadeMusic)
        {
            if (audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime;
            }
        }
        else
            if (audioSource.volume < audioVolume)
            {
                audioSource.volume += Time.deltaTime;                
            }
    }

    public IEnumerator ChangeTrack()
    {
        Debug.Log("WOrks");
        fadeMusic = true;

        yield return new WaitForSeconds(1f);
        audioSource.clip = currentTrack;

        fadeMusic = false;
        audioSource.Play();

    }

}
