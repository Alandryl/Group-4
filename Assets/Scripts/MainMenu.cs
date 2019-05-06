using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playScene;
    public GameObject BlackScreenFadeObject;

    bool disableSelection;

    void Start()
    {
        
    }

    void Update()
    {
        
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

    IEnumerator StartingGame()
    {
        disableSelection = true;
        BlackScreenFadeObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(playScene);
    }

}
