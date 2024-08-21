using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 2.0f; 
    public int totalCoinsToSpawn = 10; 

    private float spawnTimer;
    private int coinsSpawned = 0; 

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        if (coinsSpawned >= totalCoinsToSpawn)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            spawnTimer = spawnInterval;
            SpawnCoin();
            coinsSpawned++;
        }
    }

    void SpawnCoin()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points set.");
            return;
        }

        // Sıradaki spawn noktası seç
        int spawnPointIndex = coinsSpawned % spawnPoints.Length;
        Transform spawnPoint = spawnPoints[spawnPointIndex];

        // Coin'i sahnede oluştur
        Instantiate(coinPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
