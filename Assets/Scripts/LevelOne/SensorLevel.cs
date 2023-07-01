using UnityEngine;
using TMPro;

public class SensorLevel : MonoBehaviour
{
    public GameObject levelOne, doorSensors;
    public TextMeshProUGUI info, bathDoorText;
    public int dropKeyChance;
    public static bool boolean = true;

    private void OnTriggerEnter(Collider other)
    {
        levelOne.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        if (boolean)
        {
            doorSensors.gameObject.SetActive(false);
        }     
    }
}
