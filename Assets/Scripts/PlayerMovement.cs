using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    Rigidbody rb;
    Animator ac;
    public GameObject model;

    bool grounded = false;
    bool standingOnSlopeLeft;
    bool standingOnSlopeRight;

    [Header("Movement")]

    public float runSpeed;
    bool facingRight;
    public float jumpHeight;

    [Header("Ground Check")]
    public float maxRayDistance = 0.1f;
    [Range(0f, 1f)]
    public float slopeTiltLimit = 0.75f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    

    //public float slopeSlidingForce = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ac = model.GetComponent<Animator>();

        facingRight = true;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {


        //Walking

        float move = Input.GetAxis("Horizontal");

        if ((facingRight && !standingOnSlopeRight) ||
           (!facingRight && !standingOnSlopeLeft))
        {
            rb.velocity = new Vector3(move * runSpeed, rb.velocity.y, 0);
        }

        ac.SetFloat("Speed", Mathf.Abs(move));

        //Character Friction

        if (grounded && move == 0f)
        {
            GetComponent<Collider>().material.staticFriction = 2;
        }
        else
        {
            GetComponent<Collider>().material.staticFriction = 0;
        }

        //Jumping

        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            ac.SetBool("Grounded", grounded);
            rb.AddForce(new Vector3(0, jumpHeight, 0));
        }

        ac.SetFloat("verticalSpeed", rb.velocity.y);

        //Grounded

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        ac.SetBool("Grounded", grounded);


        //Flip

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }


        //Sliding

        GetAlignment();

        if (standingOnSlopeLeft == true || standingOnSlopeRight == true)
        {
            //rb.AddForce(new Vector3(0, -slopeSlidingForce, 0));
        }
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
}
