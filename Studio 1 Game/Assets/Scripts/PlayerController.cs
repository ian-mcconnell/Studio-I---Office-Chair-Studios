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
    public Animator animator;
    public Collider rightAttack;
    public Collider leftAttack;
    public bool ableToMakeADoubleJump = false; //here if we consider to add it

    //tentative health variables
    private float maxHealth = 4;
    private float currentHealth = 4;
    private bool isDead = false;

    public float invulnerabilityDuration = .3f;
    public bool isInvulnerable = false;

    public Transform pickUpPosition;

    // Start is called before the first frame update
    void Start()
    {
        rightAttack.enabled = false;
        leftAttack.enabled = false;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float hInput = Input.GetAxis("Horizontal");
        float jumpInput = Input.GetAxis("Jump");
        float vInput = Input.GetAxis("Vertical");
        direction.z = vInput * speed;
        direction.x = hInput * speed;
        direction.y += gravity * Time.deltaTime;
    
        controller.Move(direction * Time.deltaTime);
        //right
        if(direction.x > 0 || (Input.mousePosition.x > Screen.width / 2.0f))
        {
            animator.SetFloat("Speed",direction.x);
            animator.SetInteger("Position", 1); //position = 1 for right
            if (Input.GetMouseButton(0) == true)
            {
                animator.SetBool("isAttacking", true);
                rightAttack.enabled = true;
                leftAttack.enabled = false;
            }
            else if(Input.GetButtonUp("Fire1") == true)
            {
                animator.SetBool("isAttacking", false);
                rightAttack.enabled = false;

            }
        }
        else if(direction.x < 0 || (Input.mousePosition.x < Screen.width / 2.0f)) //left
        {
            animator.SetFloat("Speed", direction.x);
            animator.SetInteger("Position", 0); //position = 0 for left
            if (Input.GetMouseButton(0) == true)
            {
                animator.SetBool("isAttacking", true);
                leftAttack.enabled = true;
                rightAttack.enabled = false;
            }
            else if (Input.GetButtonUp("Fire1") == true)
            {
                animator.SetBool("isAttacking", false);
                leftAttack.enabled = false;

            }

        }//idle
        else
        {
            animator.SetFloat("Speed", 0);
            rightAttack.enabled = false;
            leftAttack.enabled = false;

        }
        

    }
    private void DoubleJump()
    {
        //Double Jump
        
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
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

    public void ChangeHealth(float amount)
    {
        if (amount >= 0 || !isInvulnerable)
        {
            currentHealth += amount;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }

        if (amount < 0)
        {
            isInvulnerable = true;
            Invoke("DisableInvulnerability", invulnerabilityDuration);
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public void DisableInvulnerability()
    {
        isInvulnerable = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ChangeLayerForward")
        {
            gameObject.layer = 2;
            GameObject.FindGameObjectWithTag("ChangeLayerForward").SetActive(false);
            GameObject.FindGameObjectWithTag("ChangeLayerBackward").SetActive(true);
        }

        if (collision.gameObject.tag == "ChangeLayerBackward")
        {
            gameObject.layer = 0;
            GameObject.FindGameObjectWithTag("ChangeLayerForward").SetActive(true);
            GameObject.FindGameObjectWithTag("ChangeLayerBackward").SetActive(false);
        }
    }

    
}
