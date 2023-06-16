using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawners : MonoBehaviour
{
    public Transform[] enemySpawners;
    public GameObject enemyPrefab;
    public bool spawned = false;

    private void FixedUpdate()
    {
        if (spawned == false)
        {
            SpawnEnemies();
            spawned = true;
        }
        
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            if (enemySpawners.Length >= enemySpawners.Length)
            {
                Instantiate(enemyPrefab, enemySpawners[i].position, enemySpawners[i].rotation);
            }
        }
    }

    private void OnEnable()
    {
        print("Enemy Spawners Script Enabled");
        spawned = false;
    }
}
