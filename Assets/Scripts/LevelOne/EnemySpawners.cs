using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawners : MonoBehaviour
{
    public GameObject enemyPrefab;
    public bool spawned = false;
    public Transform[] enemySpawners;
    public Transform player;
    public int rangeEnemySpawner;
    public int instantiated;
    public int enemiesPerWave;

    private void Start()
    {
        enemiesPerWave = 14;
    }

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


    private async void OnEnable()
    {
        await Task.Delay(20);
        spawned = false;
        gameObject.SetActive(false);
    }
}