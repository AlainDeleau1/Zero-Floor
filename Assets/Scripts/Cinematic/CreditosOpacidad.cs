using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditosOpacidad : MonoBehaviour
{
    public float duracionAparicion = 2f; // Duraci�n de la aparici�n en segundos
    public float duracionDesvanecimiento = 2f; // Duraci�n del desvanecimiento en segundos
    public float duracionDesaparicion = 2f; // Duraci�n de la desaparici�n en segundos
    public float tiempoDemora = 1f; // Tiempo de demora antes de la aparici�n en segundos

    private Image imagen;
    private float tiempoTranscurrido = 0f;
    private bool demoraCompletada = false;

    private void Start()
    {
        imagen = GetComponent<Image>();

        // Iniciar con opacidad en 0
        Color color = imagen.color;
        color.a = 0f;
        imagen.color = color;
    }

    private void Update()
    {
        if (!demoraCompletada)
        {
            if (tiempoTranscurrido < tiempoDemora)
            {
                tiempoTranscurrido += Time.deltaTime;
            }
            else
            {
                demoraCompletada = true;
                tiempoTranscurrido = 0f;
            }
        }
        else
        {
            if (tiempoTranscurrido < duracionAparicion)
            {
                // Aparici�n gradual
                tiempoTranscurrido += Time.deltaTime;

                float t = tiempoTranscurrido / duracionAparicion; // Interpolaci�n normalizada (0 a 1)

                Color color = imagen.color;
                color.a = Mathf.Lerp(0f, 1f, t);
                imagen.color = color;
            }
            else if (tiempoTranscurrido < duracionAparicion + duracionDesvanecimiento)
            {
                // Mantener imagen visible
                tiempoTranscurrido += Time.deltaTime;
            }
            else if (tiempoTranscurrido < duracionAparicion + duracionDesvanecimiento + duracionDesaparicion)
            {
                // Desaparici�n gradual
                tiempoTranscurrido += Time.deltaTime;

                float t = (tiempoTranscurrido - duracionAparicion - duracionDesvanecimiento) / duracionDesaparicion; // Interpolaci�n normalizada (0 a 1)

                Color color = imagen.color;
                color.a = Mathf.Lerp(1f, 0f, t);
                imagen.color = color;
            }
        }
    }
}
