using System.Collections;
using UnityEngine;

public class ElevatorToLevel6 : MonoBehaviour
{
    public GameController gameController;
    public BombScript bs;

    private void OnTriggerEnter(Collider other)
    {
        if (gameController != null)
        {
            if (bs.explosionPlayed == true)
            {
                gameController.ChangeScene(GameController.SceneNames.Level6);
            }
        }
        else
        {
            Debug.LogWarning("El GameController no está en la escena o no tiene el tag adecuado.");
        }
    }
}
