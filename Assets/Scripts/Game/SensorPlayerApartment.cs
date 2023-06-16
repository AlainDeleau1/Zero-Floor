using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorPlayerApartment : MonoBehaviour
{
    public bool musicIntro = false;

    private void OnTriggerStay(Collider other)
    {
        musicIntro = true;
    }

}
