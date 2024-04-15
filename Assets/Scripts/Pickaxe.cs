using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickaxe : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Drillable")) return;
        
        Tilemap tilemap = other.GetComponent<Tilemap>();
        
        if (tilemap != null)
        {
            StartCoroutine(MiningCoroutine(tilemap));
        }
    }

    private IEnumerator MiningCoroutine(Tilemap tilemap)
    {
        for (int x = 0; x <= 3; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                tilemap.SetTile(tilemap.WorldToCell(transform.position + new Vector3(x, y, 0)), null);
            }

            yield return new WaitForSeconds(0.5f);
        }
        
        Destroy(gameObject);
    }
}
