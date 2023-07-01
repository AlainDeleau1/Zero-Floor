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
        if (checkpoint != null && checkpoint.passed == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StartCoroutine(SetPlayerPosition());
        }
    }

    private IEnumerator SetPlayerPosition()
    {
        yield return new WaitForEndOfFrame(); // Espera hasta que la escena se haya cargado completamente
        player.transform.position = checkpoint.vectorPoint;
    }

    private void Start()
    {
        wonText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (kills >= 10 && levelOne.activeInHierarchy)
        {
            print("SPAWN LEVEL ONE");
            enemySpawners.gameObject.SetActive(true);
            kills = 0;
        }
        if (kills >= 10 && levelTwo.activeInHierarchy)
        {
            print("SPAWN LEVEL TWO");
            enemySpawners2.gameObject.SetActive(true);
            kills = 0;
        }
        killsText.text = killsCounter.ToString();
    }
}

