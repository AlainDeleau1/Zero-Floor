using System.Collections;
using UnityEngine;

public class ElevatorToLevel7 : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DelayScene());
        if (gameController != null)
        {
            gameController.ChangeScene(GameController.SceneNames.Level7);
        }
        else
        {
            Debug.LogWarning("El GameController no est� en la escena o no tiene el tag adecuado.");
        }
    }

    IEnumerator DelayScene()
    {
        yield return new WaitForSeconds(3f);
    }
}