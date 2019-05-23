using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            gameManager.Death();
        }
    }
}
