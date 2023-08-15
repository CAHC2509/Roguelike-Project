using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public List<GameObject> EnemiesPrefabs;

    private bool isSpawned = false;

    public bool IsSpawned { get { return isSpawned; } }

    public GameObject SpawnEnemy()
    {
        if (EnemiesPrefabs.Count == 0)
        {
            Debug.LogWarning("No enemy prefabs available.");
            return null;
        }

        int randomIndex = Random.Range(0, EnemiesPrefabs.Count);
        GameObject enemyPrefab = EnemiesPrefabs[randomIndex];
        isSpawned = true;
        return Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void ResetSpawnStatus()
    {
        isSpawned = false;
    }
}
