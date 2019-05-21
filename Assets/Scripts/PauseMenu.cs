using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public string mainMenuScene;
    public GameObject BlackScreenFadeObject;

    bool disableSelection;

    public GameObject firstSelectedButton;
    EventSystem eventSystem;

    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();

                //GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedButton, null);

                //EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedButton, null);
            }
        }
    }



    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ReturnToMainMenu()
    {
        if (!disableSelection)
        {
            Resume();
            StartCoroutine(ReturningToMainMenu());
        }
    }

    public void QuitGame()
    {
        if (!disableSelection)
        {
            Application.Quit();
        }
    }

    IEnumerator ReturningToMainMenu()
    {
        disableSelection = true;
        BlackScreenFadeObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(mainMenuScene);
    }

}
