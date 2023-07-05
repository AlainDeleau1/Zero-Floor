using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditosOpacidad : MonoBehaviour
{
    public float duracionAparicion = 2f;
    public float duracionDesvanecimiento = 2f;
    public float duracionDesaparicion = 2f;
    public float tiempoDemora = 1f;

    private Image imagen;
    private float tiempoTranscurrido = 0f;
    private bool demoraCompletada = false;

    private void Start()
    {
        imagen = GetComponent<Image>();
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
                tiempoTranscurrido += Time.deltaTime;

                float t = tiempoTranscurrido / duracionAparicion;

                Color color = imagen.color;
                color.a = Mathf.Lerp(0f, 1f, t);
                imagen.color = color;
            }
            else if (tiempoTranscurrido < duracionAparicion + duracionDesvanecimiento)
            {
                tiempoTranscurrido += Time.deltaTime;
            }
            else if (tiempoTranscurrido < duracionAparicion + duracionDesvanecimiento + duracionDesaparicion)
            {
                tiempoTranscurrido += Time.deltaTime;

                float t = (tiempoTranscurrido - duracionAparicion - duracionDesvanecimiento) / duracionDesaparicion;

                Color color = imagen.color;
                color.a = Mathf.Lerp(1f, 0f, t);
                imagen.color = color;
            }
        }
    }
}
