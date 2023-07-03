using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CambiarOpacidad : MonoBehaviour
{
    public float duracion = 2f; // Duración de la transición en segundos
    public float opacidadFinal = 1f; // Opacidad final

    private Image imagen;
    private float opacidadInicial = 0f;
    private float tiempoTranscurrido = 0f;

    private void Start()
    {
        imagen = GetComponent<Image>();

        Color color = imagen.color;
        color.a = opacidadInicial;
        imagen.color = color;
    }

    private void Update()
    {
        if (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;

            float t = tiempoTranscurrido / duracion; // Interpolación normalizada (0 a 1)

            // Cambiar opacidad usando CanvasRenderer
            CanvasRenderer canvasRenderer = imagen.canvasRenderer;
            canvasRenderer.SetAlpha(Mathf.Lerp(opacidadInicial, opacidadFinal, t));

            // Cambiar opacidad usando Image.color
            Color color = imagen.color;
            color.a = Mathf.Lerp(opacidadInicial, opacidadFinal, t);
            imagen.color = color;
        }

        if (tiempoTranscurrido >= 25)
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
    }
}
