using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTestScript : MonoBehaviour
{
    public GameObject bubbleObject;
    public bool triggersAutomatically;
    public bool oneOff = false;
    private bool hasSubmitted = false;
    private bool hasZeroed = false;
    public GameObject[] objectsToEnable; //objects that will be enabled (dialogues that should be activated) 1ST [0] OBJECT SHOULD ALWAYS BE A DIALOGUE WINDOW!
    public GameObject xIndicator;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasSubmitted )
        {
            if (!triggersAutomatically)
            {
                xIndicator.SetActive(true);
            }
            else
            {
                 EnableObjects();
                    
                    hasZeroed = false;
                    if (oneOff)
                    {
                        hasSubmitted = true;
                    }
            }
             
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            objectsToEnable[0].transform.position = new Vector3(other.transform.position.x, other.transform.position.y+2.5f, other.transform.position.z);
            if (bubbleObject != null)
            {
                bubbleObject.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 2.5f, other.transform.position.z);
            }
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
