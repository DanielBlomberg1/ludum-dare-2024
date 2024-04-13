using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource aS;

    private int delay;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        aS = GetComponent<AudioSource>();
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(1, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            GameManager.Instance.CatGoon();
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Spike"))
        {
            if(aS != null && !aS.isPlaying){
                aS.Play();
                delay = (int)aS.clip.length;
                Destroy(gameObject, delay);
            }
            
        }
    }
}
