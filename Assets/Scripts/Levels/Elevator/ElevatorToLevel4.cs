using System.Collections;
using UnityEngine;

public class ElevatorToLevel4 : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DelayScene());
        if (gameController != null)
        {
            gameController.ChangeScene(GameController.SceneNames.Level4);
        }
        else
        {
            Debug.LogWarning("El GameController no está en la escena o no tiene el tag adecuado.");
        }
    }

    IEnumerator DelayScene()
    {
        yield return new WaitForSeconds(3f);
    }
}
