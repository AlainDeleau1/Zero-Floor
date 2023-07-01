using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawners : MonoBehaviour
{
    public GameObject spawnersTutorial;

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            spawnersTutorial.gameObject.SetActive(true);
        }
    }
}
