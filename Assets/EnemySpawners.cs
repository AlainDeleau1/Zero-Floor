using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawners : MonoBehaviour
{
    public GameObject enemyPrefab;
    public bool spawned = false;
    public Transform[] enemySpawners;
    public Transform player;
    public int rangeEnemySpawner;

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
            float distance = Vector3.Distance(enemySpawners[i].position, player.position);
            if (distance > rangeEnemySpawner)
            {
                Instantiate(enemyPrefab, enemySpawners[i].position, enemySpawners[i].rotation);
                print("enemy");
            }
        }
    }

    private void OnEnable()
    {
        spawned = false;
    }
}
