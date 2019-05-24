using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [Header("Music")]
    public AudioSource audioSourceMusic;
    public AudioClip currentMusic;

    [Range(0f, 1f)]
    public float audioVolumeMusic = 1;

    public bool fadeMusic;

    [Header("Ambience")]
    public AudioSource audioSourceAmbience;
    public AudioClip currentAmbience;

    [Range(0f, 1f)]
    public float audioVolumeAmbience = 1;

    public bool fadeAmbience;


    void Start()
    {
        audioSourceMusic.volume = audioVolumeMusic;
        audioSourceMusic.clip = currentMusic;
        audioSourceMusic.Play();

        audioSourceAmbience.volume = audioVolumeAmbience;
        audioSourceAmbience.clip = currentAmbience;
        audioSourceAmbience.Play();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (fadeMusic)
        {
            if (audioSourceMusic.volume > 0)
            {
                audioSourceMusic.volume -= Time.deltaTime * 0.5f;
            }
        }
        else if (audioSourceMusic.volume < audioVolumeMusic)
        {
            audioSourceMusic.volume += Time.deltaTime;

        }

        if (fadeAmbience)
        {
            if (audioSourceAmbience.volume > 0)
            {
                audioSourceAmbience.volume -= Time.deltaTime * 0.5f;
            }
        }
        else if (audioSourceAmbience.volume < audioVolumeAmbience)
        {
            audioSourceAmbience.volume += Time.deltaTime;
        }
    }

    public IEnumerator ChangeMusic()
    {
        fadeMusic = true;

        yield return new WaitForSeconds(1f);
        audioSourceMusic.clip = currentMusic;

        fadeMusic = false;
        audioSourceMusic.Play();

    }

    public IEnumerator ChangeAmbience()
    {
        fadeAmbience = true;

        yield return new WaitForSeconds(1f);
        audioSourceAmbience.clip = currentAmbience;

        fadeAmbience = false;
        audioSourceAmbience.Play();

    }

}
