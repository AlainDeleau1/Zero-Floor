using UnityEngine;
using UnityEngine.UI;

public class CameraSensitivitySlider : MonoBehaviour
{
    public Slider sensitivitySlider;
    public GameObject cameraObject;
    public float sensitivityMultiplier = 10f;

    private float initialSensitivityX;
    private float initialSensitivityY;
    private PlayerCam playerCam;

    private void Start()
    {
        playerCam = cameraObject.GetComponent<PlayerCam>();

        initialSensitivityX = playerCam.sensX;
        initialSensitivityY = playerCam.sensY;

        sensitivitySlider.minValue = 1f;
        sensitivitySlider.maxValue = 10f;
        sensitivitySlider.value = (initialSensitivityX + initialSensitivityY) / (2f * sensitivityMultiplier);

        sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderChanged);
    }

    private void OnSensitivitySliderChanged(float value)
    {
        float sensitivity = value * sensitivityMultiplier;
        playerCam.sensX = sensitivity;
        playerCam.sensY = sensitivity;

        Debug.Log("Sensibilidad actual de la cámara (X e Y): " + sensitivity);
    }

    private void OnDestroy()
    {
        playerCam.sensX = initialSensitivityX;
        playerCam.sensY = initialSensitivityY;
    }
}



