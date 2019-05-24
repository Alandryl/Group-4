using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateActifact : MonoBehaviour
{
    public GameObject ArtifactEvent;


    // Start is called before the first frame update
    void Activate()
    {
        ArtifactEvent.GetComponent<ArtifactEvent>().Activation();
    }

    void OnEnable()
    {
        DialogueBubble.Continue += Activate;       
    }

    void OnDisable()
    {
        DialogueBubble.Continue -= Activate;   
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
