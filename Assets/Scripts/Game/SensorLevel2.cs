using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class SensorLevel2 : MonoBehaviour
{
    public GameObject levelTwo, doorSensors;
    public TextMeshProUGUI info, bathDoorText;
    public int dropKeyChance;
    public bool boolean = true;

    private async void OnTriggerExit(Collider other)
    {
        levelTwo.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        if (boolean)
        {
            doorSensors.gameObject.SetActive(false);
        }
        await Task.Delay(3000);
        info.gameObject.SetActive(false);
    }
}
