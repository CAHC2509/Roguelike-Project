using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<EnemySpawnPoint> enemiesSpawnPoints;
    public int wavesNumber = 2;
    public float firstWaveSpawnPointsUsePercentage = 0.75f;

    [SerializeField]
    private int currentWave = 0;
    private List<EnemySpawnPoint> availableSpawnPoints;

    private void Start()
    {
        GameObject enemySpawnPointsParent = GameObject.Find("Enemy Spawn Points");
        enemiesSpawnPoints = new List<EnemySpawnPoint>(enemySpawnPointsParent.GetComponentsInChildren<EnemySpawnPoint>());
        //StartNextWave();
    }

    public void StartNextWave()
    {
        currentWave++;

        if (currentWave <= wavesNumber)
        {
            if (currentWave == 1)
            {
                // Use 75% of spawn points randomly for the first wave
                int numberOfSpawnPointsToUse = Mathf.CeilToInt(enemiesSpawnPoints.Count * firstWaveSpawnPointsUsePercentage);
                availableSpawnPoints = new List<EnemySpawnPoint>(enemiesSpawnPoints);
                ShuffleSpawnPoints(availableSpawnPoints);
                availableSpawnPoints.RemoveRange(numberOfSpawnPointsToUse, availableSpawnPoints.Count - numberOfSpawnPointsToUse);
            }
            else
            {
                // Use all spawn points for subsequent waves
                availableSpawnPoints = new List<EnemySpawnPoint>(enemiesSpawnPoints);
            }

            SpawnEnemies();
        }
    }

    private void ShuffleSpawnPoints(List<EnemySpawnPoint> spawnPoints)
    {
        // Simple Fisher-Yates shuffle algorithm
        for (int i = spawnPoints.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            EnemySpawnPoint temp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[j];
            spawnPoints[j] = temp;
        }
    }

    private void SpawnEnemies()
    {
        foreach (EnemySpawnPoint spawnPoint in availableSpawnPoints)
        {
            if (!spawnPoint.IsSpawned)
            {
                spawnPoint.SpawnEnemy();
            }
        }
    }
}
