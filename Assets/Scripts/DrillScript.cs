using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrillScript : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f); 

        CameraShake.Instance.DrillCount++;
    }

    private void OnDestroy()
    {
        CameraShake.Instance.DrillCount--;
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!other.gameObject.CompareTag("Drillable")) return;
        
        Tilemap tilemap = other.gameObject.GetComponent<Tilemap>();

        if (tilemap != null)
        {
            StartCoroutine(Drill(tilemap));
        }
    }
    
    private IEnumerator Drill(Tilemap tilemap)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            
            for (int x = -1; x <= 1; x++) 
            {
                tilemap.SetTile(tilemap.WorldToCell(transform.position + new Vector3(x, -1.5f, 0)), null);
            }
        }
    }
}
