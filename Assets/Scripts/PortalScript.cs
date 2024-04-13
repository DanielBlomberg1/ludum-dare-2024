using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 1.0f;
    [SerializeField] private GameObject catPrefab;
    
    private float spawnTimer = 0.0f;
    private int spawnedAmount = 0;
    private int maxSpawnAmount;
    // Start is called before the first frame update
    void Start()
    {
        maxSpawnAmount = GameManager.Instance.GetCurrentLevelSettings().CatAmount;
    }


    void Update()
    {
        if (spawnedAmount >= maxSpawnAmount) return;
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer = 0.0f;
            Instantiate(catPrefab, transform.position, Quaternion.identity);
            spawnedAmount++;
            
        }
    }
}
