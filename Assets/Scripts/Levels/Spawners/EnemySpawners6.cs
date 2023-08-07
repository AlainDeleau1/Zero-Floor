using UnityEngine;

public class EnemySpawners6 : EnemySpawners
{
    private void Awake()
    {
        maxKills = 110;
        print(maxKills);
    }
    private void Update()
    {
        if (spawned == false)
        {
            SpawnEnemies();
            spawned = true;
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            float distance = Vector3.Distance(enemySpawners[i].position, player.position);

            if (distance > rangeEnemySpawner && instantiated <= enemiesPerWave)
            {
                int randomIndex = Random.Range(0, enemySpawners.Length);
                Vector3 randomPosition = enemySpawners[randomIndex].position;

                Instantiate(enemyPrefab, randomPosition, enemySpawners[i].rotation);
                instantiated++;
            }
        }
        instantiated = 0;
    }
}