using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject model;

    public float runSpeed;


    bool facingRight;

    Rigidbody rb;
    Animator ac;

    //Jumping
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;




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

        //Jumping

        if(grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            ac.SetBool("Grounded", grounded);
            rb.AddForce(new Vector3(0, jumpHeight, 0));
        }

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

        //Walking

        float move = Input.GetAxis("Horizontal");
        ac.SetFloat("Speed", Mathf.Abs(move));

        rb.velocity = new Vector3(move * runSpeed, rb.velocity.y, 0);

        //Jumping



        //Flip

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        model.transform.localRotation *= Quaternion.Euler(0, 180, 0);
    }
}
