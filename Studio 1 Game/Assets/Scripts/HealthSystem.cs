using UnityEngine;
using UnityEngine.UI;
public class HealthSystem : MonoBehaviour
{
    private int maxHeartNumber = 3;
    public int startHearts = 3;
    public int currentHealth;
    private int maxHealth;
    private int healthPerHeart = 4;

    
    public Image[] healthImages;
    public Sprite[] healthSprites;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHearts * healthPerHeart;
        maxHealth = maxHeartNumber * healthPerHeart;
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
    public void takeDamage(int amount)
    {
        currentHealth += amount;
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

    public void UseMedKit(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
            UpdateHearts();
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, startHearts * healthPerHeart);
        UpdateHearts();
        Destroy(gameObject);
    }
}
