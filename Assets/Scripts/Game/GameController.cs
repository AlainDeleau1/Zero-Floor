using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;


public class GameController : MonoBehaviour
{
    public int kills;

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

    private void Update()
    {
        if (kills == 23)
        {
            wonText.gameObject.SetActive(true);
            Invoke("RestartLevel", 3f);
        }
    }
}
