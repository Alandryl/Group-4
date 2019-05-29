using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playScene;
    public GameObject BlackScreenFadeObject;

    bool disableSelection;
    bool pressAnyButton;

    [Header("Objects")]

    public GameObject cameraObject;
    public GameObject musicPlayer;
    public GameObject logo;
    public GameObject pressAnyButtonText;

    [Header("Screens")]

    public GameObject mainScreen;


    void Start()
    {
        mainScreen.SetActive(false);
        StartCoroutine(StartUp());
    }

    void Update()
    {
        if (Input.anyKey && !pressAnyButton && !disableSelection)
        {
            pressAnyButton = true;
            StartCoroutine(EnterMainScreen());
        }
    }

    public void PlayGame ()
    {
        if (!disableSelection)
        {
            StartCoroutine(StartingGame());
        }
    }

    public void QuitGame ()
    {
        if (!disableSelection)
        {
            Application.Quit();
        }
    }

    IEnumerator StartUp()
    {
        disableSelection = true;
        yield return new WaitForSeconds(1);
        disableSelection = false;
    }

    IEnumerator EnterMainScreen()
    {
        disableSelection = true;
        logo.GetComponent<Animator>().SetTrigger("shrink");
        pressAnyButtonText.GetComponent<Animator>().SetTrigger("Fade");
        cameraObject.GetComponent<Animator>().SetTrigger("zoomIn");
        yield return new WaitForSeconds(1);
        mainScreen.SetActive(true);
        disableSelection = false;
    }

    IEnumerator StartingGame()
    {
        disableSelection = true;

        musicPlayer.GetComponent<MusicPlayer>().currentMusic = null;
        musicPlayer.GetComponent<MusicPlayer>().currentAmbience = null;
        musicPlayer.GetComponent<MusicPlayer>().fadeMusic = true;
        musicPlayer.GetComponent<MusicPlayer>().fadeAmbience = true;

        BlackScreenFadeObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(playScene);
    }

}
