using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource aS;

    private int delay;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
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
                StartCoroutine(DestoryAfterDelay());
            }
            
        }
    }

    IEnumerator DestoryAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
