using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour
{
    public GameController gameController;

    public void Jugar()
    {
        // Cambiar a la escena "Level0" usando el enum
        if (gameController != null)
        {
            gameController.ChangeScene(GameController.SceneNames.Level0);
        }
        else
        {
            Debug.LogWarning("El GameController no está en la escena o no tiene el tag adecuado.");
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
}
