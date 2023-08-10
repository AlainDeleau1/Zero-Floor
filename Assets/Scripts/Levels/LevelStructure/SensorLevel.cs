using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class SensorLevel : MonoBehaviour
{
    public GameObject levelOne, killsCounter;
    public TextMeshProUGUI info, bathDoorText;
    public bool triggerOnce = false;

    private async void OnTriggerExit(Collider other)
    {
        if (triggerOnce == true)
            return;

        levelOne.gameObject.SetActive(true);
        killsCounter.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        await Task.Delay(3000);
        info.gameObject.SetActive(false);
        triggerOnce = true;
    }
}
