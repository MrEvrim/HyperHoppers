using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2.0f; 

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            spawnTimer = spawnInterval;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefabs.Count == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No obstacles or spawn points set.");
            return;
        }

        // Random bir prefab seç
        int randomPrefabIndex = Random.Range(0, obstaclePrefabs.Count);
        GameObject prefab = obstaclePrefabs[randomPrefabIndex];

        // Random bir spawn noktası seç
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        // Prefab'i sahnede oluştur
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}
