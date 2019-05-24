
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;

    public delegate void ButtonPress(); //delegate , i dont really understand it, but its basically something that calls a series of functions
    public static event ButtonPress OptionA;
    public static event ButtonPress OptionB; //our events
    public static event ButtonPress Continue;

    public Color textColor = Color.white;

    public string questionText; //input for question

    public bool hasAnswers = false; //input for whether the bubble has options or not
   
    public string answerAText;   //input text for answer a
    public string answerBText;   //input text for answer b

    public bool aOpensNewDialogue; //input whether a opens new dialogue window
    public bool bOpensNewDialogue;//input whether b opens new dialogue window

    public GameObject aDialogueBubble; //reference to option a bubble
    public GameObject bDialogueBubble; //reference to option b bubble

    public GameObject continueBubble;//this only works if hasAnswers is enabled

    public Text questionTextObject; //refrence to canvas text objecta
    public Text answerTextAObject;
    public Text answerTextBObject;

    public Text indicator;  //refrence to canvas indicator object

    private Vector3 indicatorAPosition; //Vectors that indicate the positions of the indicator arrow, will be used later
    private Vector3 indicatorBPosition;

    private int selectID = 0; //the integer that manages what selection the user is currently on, starts at option 0

    private bool canChange = true; //internal boolcheck

    private bool hasZeroed = false; //internal boolcheck

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        //sets positions of arrow to the position the text objects in the y and z axis
        indicatorAPosition = new Vector3(indicator.transform.position.x, answerTextAObject.transform.position.y, answerTextAObject.transform.position.z);
        indicatorBPosition = new Vector3(indicator.transform.position.x, answerTextBObject.transform.position.y, answerTextBObject.transform.position.z);

        //sets text of canvas objects to the input text of the "question", and the answers if applicable
        questionTextObject.text = questionText;
        questionTextObject.color = textColor;
        if (hasAnswers)
        {
            answerTextAObject.text = answerAText;
            answerTextBObject.text = answerBText;
        }
        else
        {
            answerTextAObject.text = string.Empty;
            answerTextBObject.text = string.Empty;
            indicator.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
            //gets "submit" input, unlocks bool lock if zero or below
            float submitInput = Input.GetAxis("Submit");

            if (submitInput <= 0)
            {
                hasZeroed = true;
            }


            if (hasZeroed)
            {
            //input for vertical axis, changes selectID, and triggers "SwitchOptions"
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

                //this makes sure we cant scroll lightning fast through lists
                if (inputY == 0f && !canChange)
                {
                    canChange = true;
                }

            //on submit buttonpres
            if (submitInput > 0)
            {
                if (!hasAnswers)
                {
                    if (continueBubble != null)
                    {
                        continueBubble.SetActive(true);
                    }
                    else if (Continue != null)
                    {
                        
                            Continue();
                        
                    }
                    InteractTestScript interactObject = gameObject.GetComponentInParent<InteractTestScript>();

                    if (interactObject != null)
                    {
                        interactObject.bubbleObject = continueBubble;
                    } 

                    
                    
                    hasZeroed = false;
                    gameObject.SetActive(false);
                    playerMovement.movementEnabled = true;
                }
                else
                {
                    if (selectID == 0)
                    {
                        if (aOpensNewDialogue && aDialogueBubble != null)
                        {
                            InteractTestScript interactObject = gameObject.GetComponentInParent<InteractTestScript>();
                            if (interactObject != null)
                            {
                                interactObject.bubbleObject = aDialogueBubble;
                            }
                            aDialogueBubble.SetActive(true);
                            gameObject.SetActive(false);
                        }
                        else
                        {
                            if (OptionA != null)
                            {
                                OptionA();
                                

                            }
                            playerMovement.movementEnabled = true;
                            gameObject.SetActive(false);
                        }


                    }
                    else if (selectID == 1)
                    {
                        if (bOpensNewDialogue && bDialogueBubble != null)
                        {
                            InteractTestScript interactObject = gameObject.GetComponentInParent<InteractTestScript>();
                            if (interactObject != null)
                            {
                                interactObject.bubbleObject = bDialogueBubble;
                            }
                            gameObject.SetActive(false);
                            bDialogueBubble.SetActive(true);
                        }
                        else
                        {
                            if (OptionB != null)
                            {
                                OptionB();
                                
                            }
                            playerMovement.movementEnabled = true;
                            gameObject.SetActive(false);
                        }

                    }
                    hasZeroed = false;
                }
            }
         }
                    //if else statemetns determine the option selected, disables self and enables new window, according to options

  
        
        
        
    }
    void SwitchOption()
    {
        //This function takes care of styling of font and positioning of indicator arrow
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
