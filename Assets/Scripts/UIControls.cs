using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{

    public GameObject offCanvas;
    public GameObject onCanvas;
    public GameObject firstObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch()
    {
        offCanvas.SetActive(true);
    }
}
