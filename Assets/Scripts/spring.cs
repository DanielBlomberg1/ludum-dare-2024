using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float _springForce = 10f;
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Cat"))
        {
            coll.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _springForce);
        }
    }
}
