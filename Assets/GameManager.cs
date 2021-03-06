﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject currentCheckpoint;
    GameObject BlackScreenFadeObject;
    public string mainMenuScene;

    void Start()
    {
        player = GameObject.Find("Player");
        BlackScreenFadeObject = GameObject.Find("BlackScreenFade");
    }

    void Update()
    {

    }

    public void Death()
    {
        StartCoroutine (Respawn());
    }

    public void EndGame()
    {
        StartCoroutine(EndGameCoroutine());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        BlackScreenFadeObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = currentCheckpoint.transform.position;
        player.GetComponent<PlayerMovement>().ac.SetTrigger("riseUp");
        yield return new WaitForSeconds(1);
        BlackScreenFadeObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        player.GetComponent<PlayerMovement>().movementEnabled = true;
    }


    IEnumerator EndGameCoroutine()
    {
        BlackScreenFadeObject.GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(mainMenuScene);
    }
}
