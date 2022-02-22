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
    public GameObject rightAnimation;
    public GameObject leftAnimation;
    public GameObject idleAnimation;
    public GameObject rightAttack;
    public GameObject leftAttack;
    public bool ableToMakeADoubleJump = false; //here if we consider to add it

    //tentative health variables
    private float maxHealth = 4;
    private float currentHealth = 4;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {

        idleAnimation.SetActive(true);
        leftAnimation.SetActive(false);
        rightAnimation.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // Input.GetButtonDown("fire1");
        float hInput = Input.GetAxis("Horizontal");
        float jumpInput = Input.GetAxis("Jump");
        float vInput = Input.GetAxis("Vertical");
        direction.z = vInput * speed;
        direction.x = hInput * speed;
        direction.y += gravity * Time.deltaTime;
    
        controller.Move(direction * Time.deltaTime);
        if(direction.x > 0)
        {

            if (Input.GetMouseButton(0) == true)
            {
                rightAttack.SetActive(true);
                leftAnimation.SetActive(false);
                rightAnimation.SetActive(false);
                idleAnimation.SetActive(false);
            }
            else if(Input.GetButtonUp("Fire1") == true)
            {
                rightAttack.SetActive(false);
                leftAnimation.SetActive(false);
                rightAnimation.SetActive(false);
                idleAnimation.SetActive(false);
            }
            else
            {
                rightAnimation.SetActive(true);
                leftAnimation.SetActive(false);
                idleAnimation.SetActive(false);
            }

        }
        else if(direction.x < 0)
        {

            if (Input.GetMouseButton(0) == true)
            {
                leftAttack.SetActive(true);
                leftAnimation.SetActive(false);
                rightAnimation.SetActive(false);
                idleAnimation.SetActive(false);
            }
            else if (Input.GetButtonUp("Fire1") == true)
            {
                leftAttack.SetActive(false);
                leftAnimation.SetActive(false);
                rightAnimation.SetActive(false);
                idleAnimation.SetActive(false);
            }
            else
            {
                leftAnimation.SetActive(true);
                rightAnimation.SetActive(false);
                idleAnimation.SetActive(false);
            }
        }
        else
        {
            leftAnimation.SetActive(false);
            rightAnimation.SetActive(false);
            idleAnimation.SetActive(true);
            leftAttack.SetActive(false);
            rightAttack.SetActive(false);
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
            ableToMakeADoubleJump = true;
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

    public virtual void ChangeHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
