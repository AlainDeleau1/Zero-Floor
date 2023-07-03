using UnityEngine;
using TMPro;

public class SensorLevel2 : MonoBehaviour
{
    public GameObject levelTwo, doorSensors, killsCounter;
    public TextMeshProUGUI info, bathDoorText, killsCounterText;
    public GameController gc;
    public bool triggerOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggerOnce == true)
            return;

        levelTwo.gameObject.SetActive(true);
        killsCounter.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        gc.killsCounter = 0;
        doorSensors.gameObject.SetActive(false);
        triggerOnce = true;
    }
}
