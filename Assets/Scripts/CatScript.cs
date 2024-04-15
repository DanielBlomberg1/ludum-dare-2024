using UnityEngine;
using FMODUnity;

public class CatScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private StudioEventEmitter _audioSource;

    private float catSpeed = 1;

    Vector3 prevPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<StudioEventEmitter>();
        
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
                Death();
                break;
            case "Spike":
                if (_audioSource != null && !_audioSource.IsPlaying()){
                    _audioSource.Play();
                }
                Murder();
                break;
        }
    }
    private void Death(){
         Destroy(gameObject);
    }
    private void Murder(){
        GameManager.Instance.CatEdged();
        Destroy(gameObject);
    }
}
