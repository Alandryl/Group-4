using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    AudioSource audioSource;
    Rigidbody rb;
    Animator ac;
    public GameObject model;

    public bool grounded = false;
    bool standingOnSlopeLeft;
    bool standingOnSlopeRight;
    public bool canDoubleJump;

    [Header("Movement")]

    public float runSpeed = 4;
    bool facingRight;
    public float jumpHeight = 500;
    public float jumpCooldown = 0.3f;
    float jumpCooldownCounter = 0;

    [Header("Ground Check")]
    public float maxRayDistance = 0.1f;
    [Range(0f, 1f)]
    public float slopeTiltLimit = 0.75f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;



    [Header("Dash")]
    public float dashSpeed = 15;
    public float dashTime = 0.25f;
    float dashCounter;
    bool dashReady;
    public bool isDashing;
    public GameObject dashEffect;



    [Header("Audio")]
    public AudioClip audioJump;
    public AudioClip audioDash;


    //public float slopeSlidingForce = 100;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        ac = model.GetComponent<Animator>();

        facingRight = true;
    }

    void Update()
    {
        //Walking

        float move = Input.GetAxis("Horizontal");

        if ((facingRight && !standingOnSlopeRight && !isDashing) ||
           (!facingRight && !standingOnSlopeLeft && !isDashing))
        {
            rb.velocity = new Vector3(move * runSpeed, rb.velocity.y, 0);
        }

        ac.SetFloat("Speed", Mathf.Abs(move));

        //Jumping

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded && jumpCooldownCounter <= 0f)
            {
                grounded = false;
                ac.SetBool("Grounded", grounded);
                rb.AddForce(new Vector3(0, jumpHeight, 0));
                audioSource.PlayOneShot(audioJump);
                jumpCooldownCounter = jumpCooldown;
            }

            if (canDoubleJump && !grounded && jumpCooldownCounter <= 0f)
            {
                grounded = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(new Vector3(0, jumpHeight, 0));
                audioSource.PlayOneShot(audioJump);
                jumpCooldownCounter = jumpCooldown;
                canDoubleJump = false;
            }

        }

        //Dash

        if (grounded)
        {
            dashReady = true;
        }
        if (Input.GetButtonDown("Ability1") && grounded == false && dashReady)
        {
            Dash();
        }

        if (isDashing)
        {
            if (facingRight)
            {
                rb.velocity = new Vector3(dashSpeed, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(-dashSpeed, 0, 0);
            }
        }

        /*
        if (isDashing)
        {
            ac.SetBool("isDashing", true);
        }
        else
        {
            ac.SetBool("isDashing", false);
        }
        */

        //Flip

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        //Character Friction

        if (grounded && move == 0f)
        {
            GetComponent<Collider>().material.staticFriction = 2;
        }
        else
        {
            GetComponent<Collider>().material.staticFriction = 0;
        }


        ac.SetFloat("verticalSpeed", rb.velocity.y);

        if (jumpCooldownCounter > 0)
        {
            jumpCooldownCounter -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        //Animation Speed

        ac.speed = runSpeed / 4;


        //Grounded

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0 && jumpCooldownCounter <= 0f)
        {
            grounded = true;
            canDoubleJump = true;
        }
        else
        {
            grounded = false;
        }

        ac.SetBool("Grounded", grounded);


        //Sliding

        GetAlignment();

        if (standingOnSlopeLeft == true || standingOnSlopeRight == true)
        {
            //rb.AddForce(new Vector3(0, -slopeSlidingForce, 0));
        }

    }

    void Dash()
    {
        dashReady = false;
        isDashing = true;
        dashCounter = dashTime;
        audioSource.PlayOneShot(audioDash);
        StartCoroutine(Dashing());
    }


        void GetAlignment()
    {
        RaycastHit hitFront;
        RaycastHit hitMiddle;
        RaycastHit hitBack;

        Physics.Raycast(transform.position + new Vector3(0.3f, 0.5f, 0), -transform.up, out hitFront, maxRayDistance);
        Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), -transform.up, out hitMiddle, maxRayDistance);
        Physics.Raycast(transform.position + new Vector3(-0.3f, 0.5f, 0), -transform.up, out hitBack, maxRayDistance);

        Vector3 upFront = hitFront.normal;
        Vector3 upMiddle = hitMiddle.normal;
        Vector3 upBack = hitBack.normal;

        //Debug.Log(hitMiddle.normal);


        if (upFront.x >= slopeTiltLimit || upMiddle.x >= slopeTiltLimit || upBack.x >= slopeTiltLimit)
        {
            standingOnSlopeLeft = true;
        }
        else
        {
            standingOnSlopeLeft = false;
        }

        if (upFront.x <= -slopeTiltLimit || upMiddle.x <= -slopeTiltLimit || upBack.x <= -slopeTiltLimit)
        {
            standingOnSlopeRight = true;
        }
        else
        {
            standingOnSlopeRight = false;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        model.transform.localRotation *= Quaternion.Euler(0, 180, 0);
    }

    IEnumerator Dashing()
    {   
        yield return new WaitForSeconds(dashTime);
        isDashing =  false;
    }
}
