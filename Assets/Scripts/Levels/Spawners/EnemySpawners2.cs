using UnityEngine;

public class EnemySpawners2 : EnemySpawners
{
    private void Awake()
    {
        maxKills = 50;
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
            int chance = Random.Range(0, 100);
            if (distance > rangeEnemySpawner && instantiated <= enemiesPerWave)
            {
                int randomIndex = Random.Range(0, enemySpawners.Length);
                Vector3 randomPosition = enemySpawners[randomIndex].position;

                if (chance > 25)
                {
                    Instantiate(enemyPrefab, randomPosition, enemySpawners[i].rotation);
                    instantiated++;
                }
                else
                {
                    Instantiate(shooterEnemy, randomPosition, enemySpawners[i].rotation);
                    instantiated++;
                }
            }
        }
        instantiated = 0;
    }
}