using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ElevatorToNextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DelayScene());
        SceneManager.LoadScene("Level 1");
    }

    IEnumerator DelayScene()
    {
        yield return new WaitForSeconds(3f);
    }

}
