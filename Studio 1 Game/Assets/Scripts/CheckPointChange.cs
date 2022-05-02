using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointChange : MonoBehaviour
{
    public Sprite checkSprite;
    private SpriteRenderer spriteRenderer;
    public AudioSource SaveSource;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SaveSource.Play();
            spriteRenderer.sprite = checkSprite;
        }
    }
}
