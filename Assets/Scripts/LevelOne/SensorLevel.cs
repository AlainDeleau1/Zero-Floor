using UnityEngine;
using TMPro;

public class SensorLevel : MonoBehaviour
{
    public GameObject levelOne, doorSensors, killsCounter;
    public TextMeshProUGUI info, bathDoorText;
    public int dropKeyChance;
    public bool triggerOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerOnce == true)
            return;
          
        levelOne.gameObject.SetActive(true);
        killsCounter.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        doorSensors.gameObject.SetActive(false);
        triggerOnce = true;
    }
}
