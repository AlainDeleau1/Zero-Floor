using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDemo : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
