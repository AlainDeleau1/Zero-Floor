using UnityEngine;

public class ElevatorToLevel2 : MonoBehaviour
{
    public GameController gameController;
    public BombScript bs;

    private void OnTriggerEnter(Collider other)
    {
        if (gameController != null)
        {
            if (bs.explosionPlayed == true)
            {
                gameController.ChangeScene(GameController.SceneNames.Level2);
            }           
        }
        else
        {
            Debug.LogWarning("El GameController no est� en la escena o no tiene el tag adecuado.");
        }
    }
}
