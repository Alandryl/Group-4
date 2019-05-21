using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    Rigidbody rb;
    public GameObject lightObject;
    public LayerMask darkLayer;

    public bool isInDark;

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
        else
        {
            isInDark = false;
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
