using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float spawnBaseTimer;

    private void Start()
    {
        spawnTimer = GameSystemManager.Instance.enemySpawnTime;
    }

    private void Update()
    {
        if (GameSystemManager.Instance.canSpawn)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                ChooseRandomSpawnLocation();
                spawnTimer = GameSystemManager.Instance.enemySpawnTime;
            }
        }
        
        
    }
    void ChooseRandomSpawnLocation()
    {
        var number = Random.Range(0, 12);
        SpawnEnemy(spawnLocations[number]);
        
    }

    void SpawnEnemy(Transform spawnLocation)
    {
        Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
    }
}
