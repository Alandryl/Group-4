using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTestScript : MonoBehaviour
{
    public bool stopsPlayerMovement = true;
    private GameObject player;
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
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }
        if (other.tag == "Player" && !hasSubmitted )
        {
            if (!triggersAutomatically)
            {
                if (xIndicator != null)
                {
                    xIndicator.SetActive(true);
                }
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
            objectsToEnable[0].transform.position = new Vector3(other.transform.position.x, other.transform.position.y +2f- Camera.main.transform.position.z*0.1f, other.transform.position.z);
            objectsToEnable[0].transform.localScale = new Vector3 ( -Camera.main.transform.position.z*0.1f, -Camera.main.transform.position.z * 0.1f, -Camera.main.transform.position.z * 0.1f);

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
                    if (xIndicator != null)
                    {
                        xIndicator.SetActive(false);
                    }
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
            if (xIndicator != null)
            {
                xIndicator.SetActive(false);
            }
           
        }
    }

    void EnableObjects()
    {
        if (stopsPlayerMovement)
        {
            player.GetComponent<PlayerMovement>().movementEnabled = false;
        }
        
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }

    void DisableObjects()
    {
        player.GetComponent<PlayerMovement>().movementEnabled = true;
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(false);
        }
    }
}
