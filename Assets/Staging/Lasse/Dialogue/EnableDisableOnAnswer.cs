using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableOnAnswer : MonoBehaviour
{
    public GameObject[] objectsToEnableA;
    public GameObject[] objectsToEnableB;
    public GameObject[] objectsToDisableA;
    public GameObject[] objectsToDisableB;
    
    void OnEnable()
    {
        DialogueBubble.OptionA += EnableDisableObjectsA;
      
        DialogueBubble.OptionB += EnableDisableObjectsB;
        
        
    }

    void OnDisable()
    {
        DialogueBubble.OptionA -= EnableDisableObjectsA;
        DialogueBubble.OptionB -= EnableDisableObjectsB;
    }
 
    void EnableDisableObjectsA()
    {
        foreach (GameObject go in objectsToEnableA)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in objectsToDisableA)
        {
            go.SetActive(false);
        }
    }
    void EnableDisableObjectsB()
    {
        foreach (GameObject go in objectsToEnableB)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in objectsToDisableB)
        {
            go.SetActive(false);
        }
    }
}
