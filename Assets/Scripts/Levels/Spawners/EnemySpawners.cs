using System.Threading.Tasks;
using UnityEngine;

public abstract class EnemySpawners : MonoBehaviour
{
    public GameObject enemyPrefab, shooterEnemy, ninjaEnemy, bigEnemy;
    public bool spawned = false;
    public Transform[] enemySpawners;
    public Transform player;
    public int rangeEnemySpawner;
    public int instantiated;
    public int enemiesPerWave;
    public int maxKills;

    private async void OnEnable()
    {
        await Task.Delay(5);
        spawned = false;
        gameObject.SetActive(false);
    }
}
