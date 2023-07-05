using UnityEngine;
using UnityEngine.UI;

//TP2 - Felipe Nunez y Augusto Couture - Delegate
public class FOVSlider : MonoBehaviour
{
    public Camera cam;
    public Camera cam2;
    public Slider slider;

    private float defaultFOV;

    private bool isDashing = false;

    private void Start()
    {
        defaultFOV = cam.fieldOfView;

        slider.minValue = 60f;
        slider.maxValue = 120f;
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    private void OnSliderValueChanged()
    {
        if (!isDashing)
        {
            float fov = slider.value;
            cam.fieldOfView = fov;
            cam2.fieldOfView = fov;
        }
    }

    public void SetFOVFromMenu(float fov)
    {
        if (!isDashing)
        {
            slider.value = fov;
            cam.fieldOfView = fov;
            cam2.fieldOfView = fov;
        }
    }

    public void ResetFOV()
    {
        slider.value = defaultFOV;
        cam.fieldOfView = defaultFOV;
        cam2.fieldOfView = defaultFOV;
    }

    public void StartDash()
    {
        isDashing = true;
    }

    public void StopDash()
    {
        isDashing = false;
    }

    public float GetCurrentFOV()
    {
        return cam.fieldOfView;
    }
}




