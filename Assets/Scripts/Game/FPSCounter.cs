using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f;
    public Text fpsText;

    private float fpsAccumulator = 0f;
    private int framesAccumulated = 0;
    private float timeLeft;

    private void Start()
    {
        timeLeft = updateInterval;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        fpsAccumulator += Time.timeScale / Time.deltaTime;
        framesAccumulated++;

        if (timeLeft <= 0)
        {
            float fps = fpsAccumulator / framesAccumulated;

            fpsText.text = "FPS: " + Mathf.RoundToInt(fps);

            timeLeft = updateInterval;
            fpsAccumulator = 0;
            framesAccumulated = 0;
        }
    }
}


