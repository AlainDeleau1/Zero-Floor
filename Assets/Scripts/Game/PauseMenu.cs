using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;

    [SerializeField] private GameObject pauseMenu;
    private CameraShake cs;
    private SoundManager sm;
    private GameController gc;

    private void Start()
    {
        pauseMenu.SetActive(false);
        cs = FindObjectOfType<CameraShake>();
        sm = FindObjectOfType<SoundManager>();
        gc = FindObjectOfType<GameController>();
    }

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        cs.enabled = false;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        cs.enabled = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        gc.RestartLevel();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

