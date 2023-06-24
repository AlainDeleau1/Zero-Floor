using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

public class GameController : MonoBehaviour
{
    public int kills;
    public int killsCounter;
    public GameObject enemySpawners;
    public Enemy enemy;

    [SerializeField] private TextMeshProUGUI wonText, killsText;

    public async void RestartLevel()
    {
        await Task.Delay(3000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {  
        wonText.gameObject.SetActive(false);
    }

    private async void Update()
    {
        if (kills >= 9)
        {
            enemySpawners.gameObject.SetActive(true);
            await Task.Delay(20);
            enemySpawners.gameObject.SetActive(false);
            kills = 0;
        }

        killsText.text = killsCounter.ToString();
    }

    

    
}
