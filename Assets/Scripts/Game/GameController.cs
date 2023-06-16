using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;


public class GameController : MonoBehaviour
{
    public int kills;
    public GameObject enemySpawners;

    [SerializeField] private TextMeshProUGUI wonText;

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
        if (kills >= 6)
        {
            enemySpawners.gameObject.SetActive(true);
            await Task.Delay(20);
            enemySpawners.gameObject.SetActive(false);
            kills = 0;
        }
    }
}
