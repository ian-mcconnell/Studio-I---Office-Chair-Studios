using System;
using UnityEngine;
using UnityEngine.UI;
public class HealthSystem : MonoBehaviour
{
    private int maxHeartNumber = 3;
    public int startHearts = 3;
    private float currentHealth = 12;
    private float maxHealth;
    private int healthPerHeart = 4;
    public PlayerController pc;
    private float damageAmount;
    
    public Image[] healthImages;
    public Sprite[] healthSprites;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = maxHeartNumber * healthPerHeart;
        Convert.ToInt32(maxHealth);
    }

    private void Update()
    {
        currentHealth = pc.currentHealth;
        Debug.Log(currentHealth);
        Debug.Log(pc.currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
        UpdateHearts();
    }
    //void checkHealth()
    //{
    //    for(int i = 0; i < maxHeartNumber; i++)
    //    {
    //        if(s)
    //    }
    //}
    // Update is called once per frame

    private void UpdateHearts()
    {


        bool empty = false;
        int i = 0;
        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if (currentHealth >= i * healthPerHeart)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - currentHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }
    //public void takeDamage(float amount)
    //{
    //    currentHealth += amount;
    //    Convert.ToInt32(currentHealth);
    //    pc.ChangeHealth(amount);
    //    currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
    //    UpdateHearts();
    //}



    //public void OnTriggerEnter(Collider other)
    //{
    //    GameObject enemy = other.gameObject;
        
    //    if (other.tag == "Enemy")
    //    {
    //        damageAmount = enemy.GetComponent<EnemyAttack>().damage;
    //        //this.gameObject.SendMessage("ChangeHealth", damageAmount);
    //        currentHealth -= damageAmount;
    //        currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
    //        UpdateHearts();
    //    }
    //}

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        Convert.ToInt32(currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
        UpdateHearts();
        
    }

}

