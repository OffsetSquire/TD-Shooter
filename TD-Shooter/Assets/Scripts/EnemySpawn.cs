using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public int numberOfEnemies = 5; // Number of enemies to spawn
    public float spawnRadius = 10f; // Radius within which enemies will be spawned
    public float spawnInterval = 2f; // Time interval between spawns

    void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Calculate a random position within the spawn radius
            Vector2 randomSpawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Instantiate the enemy at the random position
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);

            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}