using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private CameraShake cs;
    [SerializeField] private SoundManager sm;
    private GunSystem gs;

    
    public bool paused = false;

    private void Start()
    {
        gs = FindObjectOfType<GunSystem>();
        pauseMenu.SetActive(false);
        
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
        Debug.Log("pause");
        gs.enabled = false;
        AudioListener.pause = true;
        cs.enabled = false;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Debug.Log("resume");
        //gs.enabled = true;
        AudioListener.pause = false;
        cs.enabled = true;

    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
