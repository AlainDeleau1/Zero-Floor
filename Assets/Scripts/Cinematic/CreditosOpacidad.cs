using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditosOpacidad : MonoBehaviour
{
    public float duracionAparicion = 2f; // Duración de la aparición en segundos
    public float duracionDesvanecimiento = 2f; // Duración del desvanecimiento en segundos
    public float duracionDesaparicion = 2f; // Duración de la desaparición en segundos
    public float tiempoDemora = 1f; // Tiempo de demora antes de la aparición en segundos

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
                // Aparición gradual
                tiempoTranscurrido += Time.deltaTime;

                float t = tiempoTranscurrido / duracionAparicion; // Interpolación normalizada (0 a 1)

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
                // Desaparición gradual
                tiempoTranscurrido += Time.deltaTime;

                float t = (tiempoTranscurrido - duracionAparicion - duracionDesvanecimiento) / duracionDesaparicion; // Interpolación normalizada (0 a 1)

                Color color = imagen.color;
                color.a = Mathf.Lerp(1f, 0f, t);
                imagen.color = color;
            }
        }
    }
}
