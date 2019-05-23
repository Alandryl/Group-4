using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [Header("Player Light")]

    Rigidbody rb;
    public GameObject lightObject;
    public LayerMask darkLayer;
    bool isInDark;

    [Header("Player Helmet")]
    public GameObject helmetObject;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isInDark)
        {
            lightObject.SetActive(true);
        }
        else
        {
            lightObject.SetActive(false);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == 10)
        {
            isInDark = true;
        }

        if (col.gameObject.tag == "HelmetOff")
        {
            helmetObject.SetActive(false);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 10)
        {
            isInDark = false;
        }

        if (col.gameObject.tag == "HelmetOff")
        {
            helmetObject.SetActive(true);
        }
    }


    /*
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == darkLayer)
        {
            isInDark = true;
            Debug.Log("Working");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == darkLayer)
        {
            isInDark = false;
        }
    }
    */
}
