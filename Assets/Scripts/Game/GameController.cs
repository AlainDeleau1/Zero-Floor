using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int kills;
    public GameObject enemySpawners, levelOne;
    public TextMeshProUGUI wonText, killsText;
    public BombScript bs;
    public EnemySpawners es;

    private int _killsCounter; // almacenamiento de killsCounter

    public int killsCounter // la misma variable que estaba creada arriba la cambie para ponerle el get y set
    {
        get { return _killsCounter; }
        set
        {
            _killsCounter = value;
            killsText.text = killsCounter.ToString(); // Cambie el texto del cambas del Update aca para que lo actualice cuando se cambie killsCounter
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        wonText.gameObject.SetActive(false);
        killsCounter = 0; // Lo restablece de 0 siempre que arranque la escena 
    }

    private void Update()
    {
        if (kills == 1)
            SpawnEnemiesOne();              
    }

    private void SpawnEnemiesOne()
    {
        enemySpawners.gameObject.SetActive(true);
        es.enemiesPerWave = 0;
        kills = 0;
    }
}

