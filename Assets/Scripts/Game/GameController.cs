using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int kills, killsCounter;
    public GameObject enemySpawners, levelOne, levelTwo;
    [SerializeField] private TextMeshProUGUI wonText, killsText;
    public BombScript bs;

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
    }

    private void SpawnEnemiesOne()
    {
        if (kills < 15)
            return;

        print("SPAWN LEVEL ONE");
        enemySpawners.gameObject.SetActive(true);
        kills = 0;
    }
}

