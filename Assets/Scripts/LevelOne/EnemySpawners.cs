using System.Threading.Tasks;
using UnityEngine;

//TP2 Alain Deleau - Struct
public struct EnemySpawnParameters
{
    public Vector3 position;
    public Quaternion rotation;
    public int instantiated;
}

public class EnemySpawners : MonoBehaviour
{
    public GameObject enemyPrefab;
    public bool spawned = false;
    public Transform[] enemySpawners;
    public Transform player;
    public int rangeEnemySpawner;
    public int instantiated;

    private void Update()
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
            if (distance > rangeEnemySpawner && instantiated <= 9)
            {
                EnemySpawnParameters spawnParameters = new EnemySpawnParameters
                {
                    position = enemySpawners[i].position,
                    rotation = enemySpawners[i].rotation,
                    instantiated = 0
                };

                Instantiate(enemyPrefab, spawnParameters.position, spawnParameters.rotation);
                spawnParameters.instantiated++;
                instantiated++;
            }
        }

        instantiated = 0;
    }

    private async void OnEnable()
    {
        await Task.Delay(20);
        spawned = false;
        gameObject.SetActive(false);
    }
}
