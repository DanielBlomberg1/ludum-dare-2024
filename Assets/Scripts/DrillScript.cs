using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrillScript : MonoBehaviour
{
    [SerializeField] private int drillSpeed = 2;
    [SerializeField] private int drillamount = 2;

    private float idleTimer = 0.0f;
    private readonly float maxidleTime = 5.0f;

    private AudioSource aS;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if drill is idle for 2 seconds, destroy it
        idleTimer += Time.deltaTime;
        if(idleTimer >= maxidleTime){
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Drill hit something" + other.gameObject.name);
        if(!other.gameObject.CompareTag("Drillable")) return;
        Debug.Log("Drill hit something drillable" + other.gameObject.name);
        
        //only if the collision was from the bottom
        if(other.contacts[0].normal != Vector2.up){
            return;
        }

        Tilemap tilemap = other.gameObject.GetComponent<Tilemap>();
        Vector3 hitPosition = Vector3.zero;

        if (tilemap != null && other.gameObject == other.gameObject)
        {

            foreach (ContactPoint2D hit in other.contacts)
            {
                if(drillamount <= 0){
                    break;
                }
                
                if(aS != null && !aS.isPlaying){
                    aS.Play();
                }

                idleTimer = 0;

                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                StartCoroutine(DestroyTile(tilemap, hitPosition));
            }
            drillamount--;
        }
        
    }
    private IEnumerator DestroyTile(Tilemap tilemap, Vector3 hitPosition){
        yield return new WaitForSeconds(drillSpeed);
        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
    }
}
