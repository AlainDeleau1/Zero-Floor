using System.Collections;
using UnityEngine;

public class ElevatorToLevel1 : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (gameController != null)
        {
            gameController.ChangeScene(GameController.SceneNames.Level1);         
        }
        else
        {
            Debug.LogWarning("El GameController no está en la escena o no tiene el tag adecuado.");
        }
    }
}
