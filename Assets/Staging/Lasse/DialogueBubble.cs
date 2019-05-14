using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour


{
    

    public delegate void ButtonPress();
    public static event ButtonPress OptionA;
    public static event ButtonPress OptionB;

    public string questionText;
    public string answerAText;
    public string answerBText;

    public bool aOpensNewDialogue;
    public bool bOpensNewDialogue;

    public GameObject aDialogueBubble;
    public GameObject bDialogueBubble;

    public Text questionTextObject;
    public Text answerTextAObject;
    public Text answerTextBObject;

    public Text indicator;
    
    private Vector3 indicatorAPosition;
    private Vector3 indicatorBPosition;

    private int selectID = 0;

    private bool canChange = true;

    private bool hasZeroed = false;

    void Start()
    {
        indicatorAPosition = new Vector3(indicator.transform.position.x, answerTextAObject.transform.position.y, answerTextAObject.transform.position.z);
        indicatorBPosition = new Vector3(indicator.transform.position.x, answerTextBObject.transform.position.y, answerTextBObject.transform.position.z);
        questionTextObject.text = questionText;
        answerTextAObject.text = answerAText;
        answerTextBObject.text = answerBText;
    }

    void Update()
    {
        float submitInput = Input.GetAxis("Submit");
        if (submitInput <= 0) 
        {
            hasZeroed = true;
        }


        if (hasZeroed)
        {
            float inputY = Input.GetAxis("Vertical");

            if (inputY >= 0.05f && canChange)
            {
                selectID -= 1;
                if (selectID <= -1)
                {
                    selectID = 0;
                }
                SwitchOption();
                canChange = false;
            }

            if (inputY <= -0.05f && canChange)
            {
                selectID += 1;
                if (selectID >= 2)
                {
                    selectID = 1;
                }
                SwitchOption();
                canChange = false;
            }

            if (inputY == 0f && !canChange)
            {
                canChange = true;
            }

            
            if (submitInput > 0)
            {
                if (selectID == 0)
                {
                    if (aOpensNewDialogue && aDialogueBubble != null)
                    {
                        aDialogueBubble.SetActive(true);
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        if (OptionA != null)
                        {
                            OptionA();
                            gameObject.SetActive(false);

                        }
                    }
                   
                    
                }
                else if (selectID == 1)
                {
                    if (bOpensNewDialogue && bDialogueBubble != null)
                    {
                        gameObject.SetActive(false);
                        bDialogueBubble.SetActive(true);
                    }
                    else
                    {
                        if (OptionB != null)
                        {
                            OptionB();
                            gameObject.SetActive(false);
                        }
                    }
                    
                }
                hasZeroed = false;
            }
        }
        
    }
    void SwitchOption()
    {
        switch (selectID)
        {
            case 0:
                answerTextAObject.fontStyle = FontStyle.Bold;
                answerTextBObject.fontStyle = FontStyle.Normal;
                indicator.transform.position = indicatorAPosition;
                break;
            case 1:
                answerTextAObject.fontStyle = FontStyle.Normal;
                answerTextBObject.fontStyle = FontStyle.Bold;
                indicator.transform.position = indicatorBPosition;
                break;
            default:
                break;
        }
    }
}
