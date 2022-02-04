using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;
    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool ableToMakeADoubleJump = false; //here if we consider to add it


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hInput = Input.GetAxis("Horizontal");
     //   float jumpInput = Input.GetAxis("Jump");
        float vInput = Input.GetAxis("Vertical");
        direction.z = vInput * speed;
        direction.x = hInput * speed;
       // direction.y += gravity * Time.deltaTime;

        controller.Move(direction * Time.deltaTime);

    }
    private void DoubleJump()
    {
        //Double Jump
        
        direction.y = jumpForce;
   //     ableToMakeADoubleJump = false;
    }
    private void Jump()
    {
        //Jump
        direction.y = jumpForce;
    }

    private void Update()
    {
                bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        if (isGrounded)
        {
            direction.y = -1;
         //   ableToMakeADoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;//Add Gravity
            if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }
        }
    }
}
