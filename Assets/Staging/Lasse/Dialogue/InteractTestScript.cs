using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTestScript : MonoBehaviour
{
    public bool oneOff = false;
    private bool hasSubmitted = false;
    private bool hasZeroed = false;
    public GameObject[] objectsToEnable;
    public GameObject xIndicator;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasSubmitted)
        {
            xIndicator.SetActive(true); 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            float submitInput = Input.GetAxis("Submit");
            if (submitInput <= 0)
            {
                hasZeroed = true;
            }
            
            if (hasZeroed && !hasSubmitted)
            {  
                if (submitInput > 0)
                {
                    EnableObjects();
                    xIndicator.SetActive(false);
                    hasZeroed = false;
                    if (oneOff)
                    {
                        hasSubmitted = true;
                    }
                }
            }
           

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DisableObjects();
            xIndicator.SetActive(false);
        }
    }

    void EnableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }

    void DisableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(false);
        }
    }
}
