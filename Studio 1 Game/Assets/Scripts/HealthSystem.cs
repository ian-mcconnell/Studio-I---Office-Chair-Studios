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

    
    public Image[] healthImages;
    public Sprite[] healthSprites;

    protected bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHearts * healthPerHeart;
        Convert.ToInt32(currentHealth);
        maxHealth = maxHeartNumber * healthPerHeart;
        Convert.ToInt32(maxHealth);
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
        //if (currentHealth <= 4)
        //{
        //    healthImages[1].sprite = healthSprites[0];
        //    healthImages[2].sprite = healthSprites[0];
        //    healthImages[0].sprite = healthSprites[currentHealth];
        //}
        //else if (currentHealth <= 8)
        //{
        //    healthImages[0].sprite = healthSprites[5];
        //    healthImages[2].sprite = healthSprites[0];
        //    healthImages[1].sprite = healthSprites[currentHealth - 4];
        //}
        //else
        //{
        //    healthImages[0].sprite = healthSprites[5];
        //    healthImages[1].sprite = healthSprites[5];
        //    healthImages[2].sprite = healthSprites[currentHealth - 8];
        //}

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
    public void takeDamage(float amount)
    {
        currentHealth += amount;
        Convert.ToInt32(currentHealth);
        pc.ChangeHealth(amount);
        currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
        UpdateHearts();
    }



    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            currentHealth -= 1;
            currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
            UpdateHearts();
        }
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        Convert.ToInt32(currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
        UpdateHearts();
        
    }

}

