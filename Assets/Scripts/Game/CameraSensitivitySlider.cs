using UnityEngine;
using UnityEngine.UI;

public class CameraSensitivitySlider : PlayerCam
{
    public Slider sensitivitySlider;
    public GameObject cameraObject;
    public float sensitivityMultiplier = 10f;

    private float initialSensitivityX;
    private float initialSensitivityY;
    private PlayerCam playerCam;
    private PlayerData pd;

    private void Start()
    {
        playerCam = cameraObject.GetComponent<PlayerCam>();
        pd = FindObjectOfType<PlayerData>();

        initialSensitivityX = pd.sensitivity;
        initialSensitivityY = pd.sensitivity;

        sensitivitySlider.minValue = 1f;
        sensitivitySlider.maxValue = 10f;
        sensitivitySlider.value = 5f;

        sensitivitySlider.onValueChanged.AddListener(OnSensitivitySliderChanged);
    }

    private void OnSensitivitySliderChanged(float value)
    {
        float sensitivity = value * sensitivityMultiplier;
        sensX = sensitivity;
        sensY = sensitivity;

        pd.sensitivity = sensitivity;
    }
}



