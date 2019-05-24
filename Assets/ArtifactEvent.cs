using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArtifactEvent : MonoBehaviour
{
    AudioSource audioSource;

    public bool activated;


    public AudioClip audioTension;
    GameObject player;
    public GameObject BlackScreenFadeObject;
    public GameObject particleEffects;
    public GameObject lightObject;



    public GameObject musicPlayerObject;
    MusicPlayer musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        audioSource = GetComponent<AudioSource>();
        particleEffects.SetActive(false);

        musicPlayerObject = GameObject.Find("MusicPlayer");
        musicPlayer = musicPlayerObject.GetComponent<MusicPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            lightObject.GetComponent<Light>().intensity += 0.5f * Time.deltaTime;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !activated)
        {    
            player = other.gameObject;
            player.GetComponent<PlayerMovement>().movementEnabled = false;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            StartCoroutine(Activate());
            activated = true;
        }
    }
    */

    public void Activation()
    {
        player.GetComponent<PlayerMovement>().movementEnabled = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        StartCoroutine(Activate());
        activated = true;
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerMovement>().movementEnabled = false;
        audioSource.PlayOneShot(audioTension);
        musicPlayer.fadeMusic = true;
        particleEffects.SetActive(true);
        yield return new WaitForSeconds(11);
        BlackScreenFadeObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(3);
        BlackScreenFadeObject.GetComponent<Animator>().SetTrigger("Fade");
        particleEffects.SetActive(false);
        lightObject.SetActive(false);
        musicPlayer.fadeMusic = false;
        player.GetComponent<PlayerMovement>().ac.SetTrigger("lyingDown");
        yield return new WaitForSeconds(2);
        player.GetComponent<PlayerMovement>().ac.SetTrigger("riseUp");
        yield return new WaitForSeconds(1);
        player.GetComponent<PlayerMovement>().movementEnabled = true;
        player.GetComponent<PlayerMovement>().doubleJumpUnlocked = true;
        player.GetComponent<PlayerMovement>().dashJumpUnlocked = true;
    }
}
