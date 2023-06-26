using UnityEngine;

public class EnemySpawners2 : MonoBehaviour
{
    public GameObject mediumEnemy, shooterEnemy;
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
            int chance = Random.Range(0, 100);
            if (distance > rangeEnemySpawner && instantiated <= 8 && chance < 50)
            {
                Instantiate(mediumEnemy, enemySpawners[i].position, enemySpawners[i].rotation);
                instantiated++;
                print("medium enemy");
            }
            else
            {
                Instantiate(shooterEnemy, enemySpawners[i].position, enemySpawners[i].rotation);
                instantiated++;
                print("shooter enemy");
            }
        }
        instantiated = 0;
    }

    private void OnEnable()
    {
        spawned = false;
    }
}
