using UnityEngine;
using System.Collections;

public class TurnColorScript : MonoBehaviour
{
    void OnEnable()
    {
        DialogueBubble.OptionA += TurnColor;
        DialogueBubble.OptionB += TurnColor2;
    }


    void OnDisable()
    {
        DialogueBubble.OptionA -= TurnColor;
        DialogueBubble.OptionB -= TurnColor2;
    }


    void TurnColor()
    {
        Renderer renderer = this.GetComponent<Renderer>();
        Color col = Color.green;
        renderer.material.color = col;
    }
    void TurnColor2()
    {
        Renderer renderer = this.GetComponent<Renderer>();
        Color col = Color.blue;
        renderer.material.color = col;
    }
}
