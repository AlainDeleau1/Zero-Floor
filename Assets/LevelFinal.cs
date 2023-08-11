using UnityEngine;

public class LevelFinal : MonoBehaviour
{
    public GameController gameController;
    public BombScript bs;
    
    void Update()
    {
        if (gameController != null)
        {
            if (bs.explosionPlayed == true)
            {
                gameController.ChangeScene(GameController.SceneNames.FinalScene);
            }
        }
        else
        {
            Debug.LogWarning("El GameController no está en la escena o no tiene el tag adecuado.");
        }
    }

}
