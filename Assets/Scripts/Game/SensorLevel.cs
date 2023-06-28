using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class SensorLevel : MonoBehaviour
{
    public GameObject levelOne, doorSensors;
    public TextMeshProUGUI info, bathDoorText;
    public int dropKeyChance;
    public static bool boolean = true;

    private async void OnTriggerExit(Collider other)
    {
        levelOne.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        if (boolean)
        {
            doorSensors.gameObject.SetActive(false);
            await Task.Delay(3000);
            info.gameObject.SetActive(false);
        }     
    }
}
