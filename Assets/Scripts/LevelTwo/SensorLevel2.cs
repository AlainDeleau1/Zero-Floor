using UnityEngine;
using TMPro;

public class SensorLevel2 : MonoBehaviour
{
    public GameObject levelTwo, doorSensors;
    public TextMeshProUGUI info, bathDoorText;
    public bool boolean = true;

    private void OnTriggerEnter(Collider other)
    {
        levelTwo.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        if (boolean)
        {
            doorSensors.gameObject.SetActive(false);
        }
    }
}
