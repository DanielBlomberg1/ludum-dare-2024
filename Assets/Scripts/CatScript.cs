using UnityEngine;

public class CatScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource aS;

    private int delay;

    private float catSpeed = 1;

    Vector3 prevPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        aS = GetComponent<AudioSource>();
        if(GameManager.Instance) catSpeed = GameManager.Instance.GetCurrentLevelSettings().CatSpeed;

        prevPos = new Vector3(0, 0, 22222);
    }
    
    private void FixedUpdate()
    {
        if(GameManager.Instance && GameManager.Instance.CurrentState != GameState.Play) return;
        
        if(prevPos == transform.position){
            catSpeed = -catSpeed; 
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, 1, 1);
        } 
  
        rb.velocity = new Vector2(catSpeed, rb.velocity.y);

        prevPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Flag":
                GameManager.Instance.CatGoon();
                Destroy(gameObject);
                break;
            case "Spike":
                if (aS != null && !aS.isPlaying){
                    aS.Play();
                    delay = (int)aS.clip.length;
                }
                Destroy(gameObject, delay);
                break;
        }
    }
}
