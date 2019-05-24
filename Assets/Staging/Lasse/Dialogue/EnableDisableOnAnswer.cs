using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableOnAnswer : MonoBehaviour
{
    public GameObject[] objectsToEnableA;
    public GameObject[] objectsToEnableB;
    public GameObject[] objectsToDisableA;
    public GameObject[] objectsToDisableB;
    public GameObject[] objectsToEnableC;
    public GameObject[] objectsToDisableC;

    void OnEnable()
    {
        DialogueBubble.OptionA += EnableDisableObjectsA;
      
        DialogueBubble.OptionB += EnableDisableObjectsB;

        DialogueBubble.Continue += EnableDisableObjectsC;


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
    void EnableDisableObjectsC()
    {
        foreach (GameObject go in objectsToEnableC)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in objectsToDisableC)
        {
            go.SetActive(false);
        }
    }
}
