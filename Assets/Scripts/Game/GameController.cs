using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int kills, killsCounter;
    public GameObject enemySpawners, enemySpawners2, levelOne, levelTwo;
    [SerializeField] private TextMeshProUGUI wonText, killsText;
    public Checkpoint checkpoint;
    public Transform checkpointTransform;
    public GameObject player;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        wonText.gameObject.SetActive(false);
    }

    private void Update()
    {
        SpawnEnemiesOne();
         
        SpawnEnemiesTwo();
        
        killsText.text = killsCounter.ToString();
    }

    private void SpawnEnemiesOne()
    {
        if (kills < 10 || !levelOne.activeInHierarchy)
            return;

        print("SPAWN LEVEL ONE");
        enemySpawners.gameObject.SetActive(true);
        kills = 0;
    }
    private void SpawnEnemiesTwo()
    {
        if (kills < 10 || !levelTwo.activeInHierarchy)
            return;

        print("SPAWN LEVEL TWO");
        enemySpawners2.gameObject.SetActive(true);
        kills = 0;
        
    }
}

