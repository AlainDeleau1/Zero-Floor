using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int kills;
    public GameObject enemySpawners, levelOne;
    public TextMeshProUGUI wonText, killsText;
    public BombScript bs;
    private int _killsCounter;

    public int killsCounter
    {
        get { return _killsCounter; }
        set
        {
            _killsCounter = value;
            killsText.text = killsCounter.ToString();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
        wonText.gameObject.SetActive(false);
        killsCounter = 0;
    }

    private void HandlePlayerDeath()
    {
        RestartLevel();
        Debug.Log("El jugador ha muerto.");
    }

    private void Update()
    {
        if (kills >= 8)
            SpawnEnemiesOne();
    }

    private void SpawnEnemiesOne()
    {
        enemySpawners.gameObject.SetActive(true);
        kills = 0;
        print("entro");
    }

    public enum SceneNames
    {
        Cinematica,
        MenuPrincipal,
        Level0,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        FinalScene
    }

    public void ChangeScene(SceneNames sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
