using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public GameManager gameManager;
    public bool activated;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && !activated)
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Respawner");

            foreach (GameObject go in gameObjectArray)
            {
                go.GetComponent<RespawnPoint>().activated = false;
            }

            activated = true;
            gameManager.currentCheckpoint = gameObject;
        }
    }
}
