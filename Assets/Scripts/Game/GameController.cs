using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int kills, killsCounter;
    public GameObject enemySpawners, enemySpawners2, levelOne, levelTwo;
    [SerializeField] private TextMeshProUGUI wonText, killsText;
    public BombScript bs;
    public BombScript2 bs2;

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
        if (bs.levelOneIsActive == true)       
            SpawnEnemiesOne();  
        else if (bs2.levelTwoIsActive == true)
            SpawnEnemiesTwo();       
        killsText.text = killsCounter.ToString();
    }

    private void SpawnEnemiesOne()
    {
        if (kills < 10)
            return;

        print("SPAWN LEVEL ONE");
        enemySpawners.gameObject.SetActive(true);
        kills = 0;
    }
    private void SpawnEnemiesTwo()
    {
        if (kills < 10)
            return;

        print("SPAWN LEVEL TWO");
        enemySpawners2.gameObject.SetActive(true);
        kills = 0;
        
    }
}

