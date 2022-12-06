using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Rigidbody chemicalAttack;
    public bool hasChemical;
    public InventorySystem inventory;
    public int npcSaved = 0;
    public int killCount = 0;

    public AudioSource BossSource;
    public AudioSource PickUpSound;

    public bool ableToMakeADoubleJump = false; //here if we consider to add it

    //tentative health variables
    private float maxHealth = 12;
    public float currentHealth;
    private bool isDead;
    public int level;
    public bool hasLoadedBefore = false;

    public float invulnerabilityDuration = .3f;
    public bool isInvulnerable = false;

    public Transform pickUpPosition;

    public bool talkedToCoachHedge = false;
    public dialogueManagement talking;

    public grabAnimationTrigger tentAnim;

    public bool hasSkullChain = false;
    public bool scienceLabopen = false;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void UpdateLevel()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Level: " + level);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void loadLevelUpdate()
    {

        PlayerData data = SaveSystem.loadPlayer();
        if (data.level == SceneManager.GetActiveScene().buildIndex )
        {
            hasLoadedBefore = data.hasLoadedBefore;
        }
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();

        level = data.level;
        currentHealth = data.health;
  //      Vector3 position;
    //    position.x = data.
     
        direction.x = data.position[0];
        direction.y = data.position[1];
        direction.z = data.position[2];
        transform.position = direction;
        controller.center = direction - transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //loadLevelUpdate();
        rightAttack.enabled = false;
        leftAttack.enabled = false;

        Renderer sortLayer = GetComponent<SpriteRenderer>();
        sortLayer.sortingLayerName = "player";
        sortLayer.sortingOrder = 2;

        speed = 8;

        //if(hasLoadedBefore == true)
        //{
        //    LoadPlayer();
        //}
            
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(direction.z);
        float hInput = Input.GetAxis("Horizontal");
        float jumpInput = Input.GetAxis("Jump");
        float vInput = Input.GetAxis("Vertical");
        direction.z = vInput * speed;
        direction.x = hInput * speed;
        direction.y += gravity * Time.deltaTime;
    
        controller.Move(direction * Time.deltaTime);
        //right 
        if(direction.x > 0 || ((Input.mousePosition.x > Screen.width / 2.0f) && !leftAttack.enabled)|| Input.GetKeyDown(KeyCode.Period))
        {
            animator.SetFloat("VSpeed", direction.z);
            animator.SetFloat("Speed",direction.x);
            animator.SetInteger("Position", 1); //position = 1 for right
            if (Input.GetButtonDown("Fire1") == true || Input.GetKeyDown(KeyCode.Period))
            {
                //if((Input.mousePosition.x < Screen.width / 2.0f) || Input.GetKeyDown(KeyCode.Comma))
                //{
                //    direction.x = direction.x*-1;
                //    animator.SetInteger("Position", 0);
                //    animator.SetFloat("Speed", direction.x*-1);
                //}
                animator.SetBool("isAttacking", true);
//                rightAttack.enabled = true;
                leftAttack.enabled = false;
                speed = 6;
            }
            else if(Input.GetButtonUp("Fire1") == true || Input.GetKeyUp(KeyCode.Period) || Input.GetKeyUp(KeyCode.Comma))
            {
                animator.SetBool("isAttacking", false);
                rightAttack.enabled = false;
                speed = 8;
            }
            else if (Input.GetButtonDown("Fire2") == true && hasChemical == true)
            {

                animator.SetBool("isRangedAttacking", true);
                Rigidbody clone;
                clone = Instantiate(chemicalAttack, new Vector3(transform.position.x+1, transform.position.y+1 , transform.position.z), transform.rotation);
                clone.velocity = transform.TransformDirection(Vector3.right *10);
                hasChemical = false;
                speed = 4;
            }
            else if (Input.GetButtonUp("Fire2") == true)
            {
                animator.SetBool("isRangedAttacking", false);
                speed = 8;
            }
        }//left
        else if(direction.x < 0 || ((Input.mousePosition.x < Screen.width / 2.0f) && !rightAttack.enabled) || Input.GetKeyDown(KeyCode.Comma))
        {
            animator.SetFloat("VSpeed", direction.z);
            animator.SetFloat("Speed", direction.x);
            animator.SetInteger("Position", 0); //position = 0 for left
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Comma))
            {
                //if ((Input.mousePosition.x > Screen.width / 2.0f) || Input.GetKeyDown(KeyCode.Period))
                //{
                //    direction.x = Mathf.Abs(direction.x);
                //    animator.SetInteger("Position", 1);
                //    animator.SetFloat("Speed",Mathf.Abs( direction.x));

                //}
                animator.SetBool("isAttacking", true);
                rightAttack.enabled = false;
                speed = 6;
            }
            else if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Comma) || Input.GetKeyUp(KeyCode.Period))
            {
                animator.SetBool("isAttacking", false);
                leftAttack.enabled = false;
                speed = 8;

            }
            else if (Input.GetButtonDown("Fire2") && hasChemical)
            {
                animator.SetBool("isRangedAttacking", true);
                Rigidbody clone;
                clone = Instantiate(chemicalAttack, new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z), transform.rotation);
                clone.velocity = transform.TransformDirection(Vector3.left * 10);
                hasChemical = false;
                speed = 4;
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                animator.SetBool("isRangedAttacking", false);
                speed = 8;
            }

        }                
        else//idle
        {
            animator.SetFloat("VSpeed", direction.z);
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
    public void SetHasChemical()
    {
        hasChemical = true;
        PickUpSound.Play();
    }
    private void Update()
    {
        //cheats
        if (Input.GetKeyDown(KeyCode.P))
        {
            npcSaved++;
        }        
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            killCount++;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(8);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(9);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene(10);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SceneManager.LoadScene(11);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SceneManager.LoadScene(12);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            SceneManager.LoadScene(13);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            SceneManager.LoadScene(14);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            SceneManager.LoadScene(15);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            SceneManager.LoadScene(9);
        }

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
        //Debug.Log(amount);
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
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            currentHealth = 0;
            isDead = true;
            LoadPlayer();
            inventory.loadInv();
            var items = FindObjectsOfType<Item>();
            foreach (Item i in items)
            {
                i.LoadItem();
            }

        }

        if (amount < 0)
        {
            SendMessage("Flash");
            isInvulnerable = true;
            Invoke("DisableInvulnerability", invulnerabilityDuration);
        }
    }
    public void GetHeldDown(int heldDown)
    {
        if(heldDown == 1)
        {
            rightAttack.enabled = false;
        }
        if(heldDown == 0)
        {
            leftAttack.enabled = false;
        }
        if(heldDown == 2)
        {
            rightAttack.enabled = true;
           
        }
        if (heldDown == 3)
        {
            leftAttack.enabled = true;

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

    IEnumerator destroyObject(GameObject target)
    {
        yield return new WaitWhile(() => talking.isTalking == true);
        Debug.Log("doneTalking");
        Destroy(target);
    }
    IEnumerator saveItems()
    {
        var items = FindObjectsOfType<Item>();
            foreach(Item i in items){
                i.SaveItem();
            }
        yield return null;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Save")
        {
            StartCoroutine(saveItems());
            hasLoadedBefore = true;
            SavePlayer();
            inventory.SaveInven();
            
        }
        if (collision.gameObject.tag == "Start")
        {
            UpdateLevel();
            SavePlayer();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "End")
        {
            //UpdateLevel();
            //SavePlayer();
            LoadNextLevel();

        }
        if(collision.gameObject.tag == "Boss")
        {
            BossSource.Play();
        }
        if (collision.gameObject.tag == "NPC")
        {
            collision.gameObject.GetComponent<DialogueTrigger>().callDialogue();
        }

        if (collision.gameObject.tag == "saveNPC")
        {
            collision.gameObject.GetComponent<DialogueTrigger>().callDialogue();
            collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            npcSaved++;
            killCount = 0;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "killReset")
        {
            killCount = 0;
        }

        if (collision.gameObject.tag == "Text")
        {
            collision.gameObject.GetComponent<DialogueTrigger>().callDialogue();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "scienceLabText")
        {
            collision.gameObject.GetComponent<DialogueTrigger>().callDialogue();
            Destroy(collision.gameObject);
            scienceLabopen = true;
        }

        if (collision.gameObject.tag == "CoachText")
        {
            collision.gameObject.GetComponent<DialogueTrigger>().callDialogue();
            talkedToCoachHedge = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "animation")
        {
            collision.GetComponent<grabAnimationTrigger>().playAnimation();
        }

        if (collision.gameObject.tag == "item")
        {
            hasSkullChain = true;
            Destroy(collision.gameObject);
        }    
    }

}
