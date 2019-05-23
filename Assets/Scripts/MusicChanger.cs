using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public GameObject musicPlayerObject;
    MusicPlayer musicPlayer;

    public AudioClip musicToPlay;

    void Start()
    {
        musicPlayerObject = GameObject.Find("MusicPlayer");
        musicPlayer = musicPlayerObject.GetComponent<MusicPlayer>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && musicPlayer.currentTrack != musicToPlay)
        {
            musicPlayer.currentTrack = musicToPlay;
            StartCoroutine(musicPlayer.ChangeTrack());
        }
    }

}
