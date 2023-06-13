using UnityEngine;
using UnityEngine.UI;

public class FOVSlider : MonoBehaviour
{
    public Camera cam;
    public Camera cam2;
    public Slider slider;

    private float defaultFOV; // Valor del FOV por defecto

    private bool isDashing = false; // Variable para controlar si se est� dasheando

    private void Start()
    {
        defaultFOV = cam.fieldOfView; // Guardamos el valor por defecto del FOV

        slider.minValue = 60f;
        slider.maxValue = 120f;

        // Configuramos el listener solo si se modifica el FOV desde el slider
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    private void OnSliderValueChanged()
    {
        if (!isDashing) // Solo se actualiza el FOV si no se est� dasheando
        {
            float fov = slider.value;
            cam.fieldOfView = fov;
            cam2.fieldOfView = fov;
        }
    }

    // M�todo para establecer el FOV desde el men� de pausa
    public void SetFOVFromMenu(float fov)
    {
        if (!isDashing) // Solo se actualiza el FOV si no se est� dasheando
        {
            slider.value = fov;
            cam.fieldOfView = fov;
            cam2.fieldOfView = fov;
        }
    }

    // M�todo para restaurar el FOV por defecto
    public void ResetFOV()
    {
        slider.value = defaultFOV;
        cam.fieldOfView = defaultFOV;
        cam2.fieldOfView = defaultFOV;
    }

    // M�todo para indicar que se est� iniciando el dasheo
    public void StartDash()
    {
        isDashing = true;
    }

    // M�todo para indicar que se ha detenido el dasheo
    public void StopDash()
    {
        isDashing = false;
    }

    // M�todo para obtener el FOV actual
    public float GetCurrentFOV()
    {
        return cam.fieldOfView;
    }
}




