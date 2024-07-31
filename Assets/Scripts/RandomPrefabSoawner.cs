using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs;

    public Transform spawnAreaCenter;

    public Vector3 spawnAreaSize;

    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 3.0f;

    private void Start()
    {
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("SpawnPrefab", spawnTime);
    }

    // Prefab spawn et
    private void SpawnPrefab()
    {
        int prefabIndex = Random.Range(0, prefabs.Length);
        GameObject prefabToSpawn = prefabs[prefabIndex];

        // Rastgele bir konum belirle
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        spawnPosition += spawnAreaCenter.position;

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Bir sonraki spawn
        ScheduleNextSpawn();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter.position, spawnAreaSize);
    }
}
