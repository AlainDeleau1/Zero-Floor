using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffIllestVillains : MonoBehaviour
{
    public AudioSource illestVillains;

    private void OnTriggerEnter(Collider other)
    {
        illestVillains.Stop();
    }
}
