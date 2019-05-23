using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    AudioSource audioSource;
    Rigidbody rb;
    public GameManager gameManager;

    public Animator ac;
    public GameObject model;

    public bool grounded = false;
    bool standingOnSlopeLeft;
    bool standingOnSlopeRight;

    [Header("Movement")]

    public bool movementEnabled = true;
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

    [Header("Falling")]
    public float airTime;
    public float hardLanding = 1f;
    public float deathFallTime = 3f;

    [Header("Double Jump")]

    public bool doubleJumpUnlocked;
    public bool canDoubleJump;

    [Header("Dash")]

    public float dashSpeed = 15;
    public float dashTime = 0.25f;
    float dashCounter;
    bool dashReady;
    public bool isDashing;



    [Header("Audio")]
    public AudioClip audioFootstep;
    public AudioClip audioJump;
    public AudioClip audioDoubleJump;
    public AudioClip audioDash;
    public AudioClip audioLanding;

    [Header("Effects")]
    public GameObject doubleJumpEffect;
    public GameObject dashEffect;
    public GameObject landingEffect;
    public GameObject hardLandingEffect;


    //public float slopeSlidingForce = 100;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        ac = model.GetComponent<Animator>();

        facingRight = true;
    }

    void Update()
    {

        //Walking

        float move = Input.GetAxis("Horizontal");

        if ((facingRight && !standingOnSlopeRight && !isDashing) && movementEnabled ||
           (!facingRight && !standingOnSlopeLeft && !isDashing && movementEnabled))
        {
            rb.velocity = new Vector3(move * runSpeed, rb.velocity.y, 0);
        }

        if (movementEnabled)
        {
            ac.SetFloat("Speed", Mathf.Abs(move));
        }
        else
        {
            ac.SetFloat("Speed", 0);
        }

        //Jumping

        if (Input.GetButtonDown("Jump") && movementEnabled && !isDashing)
        {
            if (grounded && jumpCooldownCounter <= 0f)
            {
                grounded = false;
                ac.SetBool("Grounded", grounded);
                rb.AddForce(new Vector3(0, jumpHeight, 0));
                audioSource.PlayOneShot(audioJump);
                jumpCooldownCounter = jumpCooldown;
                landingEffect.GetComponent<ParticleSystem>().Play();
            }

            if (canDoubleJump && !grounded && jumpCooldownCounter <= 0f && doubleJumpUnlocked)
            {
                grounded = false;
                rb.velocity = Vector3.zero;
                rb.AddForce(new Vector3(0, jumpHeight, 0));
                audioSource.PlayOneShot(audioDoubleJump);
                jumpCooldownCounter = jumpCooldown;
                canDoubleJump = false;
                ac.SetTrigger("doubleJump");
                doubleJumpEffect.GetComponent<ParticleSystem>().Play();
            }
        }

        //Dash

        if (grounded)
        {
            dashReady = true;
        }
        if (Input.GetButtonDown("Ability1") && grounded == false && dashReady && movementEnabled)
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

            ac.SetBool("Dashing", true);
        }
        else
        {
            ac.SetBool("Dashing", false);
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

        if (move > 0 && !facingRight && movementEnabled)
        {
            Flip();
        }
        else if (move < 0 && facingRight && movementEnabled)
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

        //Grounded

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (groundCollisions.Length > 0)
        {
            if (!grounded)
            {
                if (airTime >= hardLanding * 0.5f && airTime < hardLanding)
                {
                    landingEffect.GetComponent<ParticleSystem>().Play();
                    audioSource.PlayOneShot(audioLanding);
                }

                if (airTime >= hardLanding && airTime < deathFallTime)
                {
                    hardLandingEffect.GetComponent<ParticleSystem>().Play();
                    audioSource.PlayOneShot(audioLanding);
                    ac.SetTrigger("landing");
                    rb.velocity = Vector3.zero;
                    StartCoroutine(HardLanding());
                }

                if (airTime >= deathFallTime)
                {
                    hardLandingEffect.GetComponent<ParticleSystem>().Play();
                    audioSource.PlayOneShot(audioLanding);
                    movementEnabled = false;
                    rb.velocity = Vector3.zero;
                    Death();
                }

                grounded = true;
                canDoubleJump = true;
            }
        }

        else
        {
            grounded = false;
        }

        ac.SetBool("Grounded", grounded);

    }

    void FixedUpdate()
    {
        //Movement Enabled

        if (!movementEnabled)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        //Falling

        if (!grounded && rb.velocity.y < 0)
        {
            airTime += Time.deltaTime;
        }
        if (grounded || rb.velocity.y >= 0)
        {
            airTime = 0f;
        }

        //Animation Speed

        ac.speed = runSpeed / 4;


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
        dashEffect.GetComponent<ParticleSystem>().Play();
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

    public void Footstep()
    {
        audioSource.PlayOneShot(audioFootstep);
    }

    IEnumerator HardLanding()
    {
        movementEnabled = false;
        yield return new WaitForSeconds(1);
        movementEnabled = true;
    }

    void Death()
    {
        ac.SetTrigger("lyingDown");
        gameManager.Death();
    }
}
