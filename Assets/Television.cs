using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : MonoBehaviour
{
    public SensorPlayerApartment spa;
    public AudioSource audioSource;
    void Start()
    {
        
    }

    void Update()
    {
        if (spa.musicIntro == true)
        {
            audioSource.Stop();
        }
    }
}
