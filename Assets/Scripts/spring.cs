using FMODUnity;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float _springForce = 10f;
    
    [SerializeField] private EventReference _catSpringAudio;
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Cat"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(_catSpringAudio);
            
            coll.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _springForce);
        }
    }
}
