using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int kills, killsCounter;
    public GameObject enemySpawners, levelOne;
    public TextMeshProUGUI wonText, killsText;
    public BombScript bs;
    public EnemySpawners es;

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
        killsText.text = killsCounter.ToString();
        if (kills == 1)
            SpawnEnemiesOne();              
    }

    private void SpawnEnemiesOne()
    {
        enemySpawners.gameObject.SetActive(true);
        es.enemiesPerWave = 0;
        kills = 0;
        print("entro");
    }

    public enum SceneNames
    {
        Cinematica,
        MenuPrincipal,
        Level0,
        Level1,
        FinalScene
    }

    public void ChangeScene(SceneNames sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}

