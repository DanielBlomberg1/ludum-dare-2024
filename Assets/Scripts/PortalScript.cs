using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private float spawnDelay;
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private GameObject darkCatPrefab;
    
    private float spawnTimer = 0.0f;
    private int spawnedAmount = 0;
    private int maxSpawnAmount;
    // Start is called before the first frame update
    void Start()
    {
        maxSpawnAmount = GameManager.Instance.GetCurrentLevelSettings().CatAmount;
        spawnDelay = GameManager.Instance.GetCurrentLevelSettings().spawnDelay;
        spawnTimer = spawnDelay;
    }


    void Update()
    {
        if (spawnedAmount >= maxSpawnAmount) return;
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer = 0.0f;
            if(GameManager.Instance.GetCurrentLevelSettings().isDark) Instantiate(darkCatPrefab, transform.position, Quaternion.identity);
            else Instantiate(catPrefab, transform.position, Quaternion.identity);
            
            spawnedAmount++;
        }
    }
}
