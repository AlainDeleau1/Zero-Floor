using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CambiarOpacidad : MonoBehaviour
{
    public float duracion = 2f;
    public float opacidadFinal = 1f;

    private Image imagen;
    private float opacidadInicial = 0f;
    private float tiempoTranscurrido = 0f;

    public GameController gameController;

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

            float t = tiempoTranscurrido / duracion;

            CanvasRenderer canvasRenderer = imagen.canvasRenderer;
            canvasRenderer.SetAlpha(Mathf.Lerp(opacidadInicial, opacidadFinal, t));

            Color color = imagen.color;
            color.a = Mathf.Lerp(opacidadInicial, opacidadFinal, t);
            imagen.color = color;
        }

        if (tiempoTranscurrido >= 25)
        {
            if (gameController != null)
            {
                // Cambiar a la escena "MainMenu" usando el enum
                gameController.ChangeScene(GameController.SceneNames.MenuPrincipal);
            }
            else
            {
                Debug.LogWarning("El GameController no está asignado en el Inspector.");
            }
        }
    }
}
