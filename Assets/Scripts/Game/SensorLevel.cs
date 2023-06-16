
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class SensorLevel : MonoBehaviour
{
    public GameObject levelOne;
    public TextMeshProUGUI info;

    private async void OnTriggerStay(Collider other)
    {
        levelOne.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
        await Task.Delay(4000);
        info.gameObject.SetActive(true);
    }

}
