using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCoroutine : MonoBehaviour
{
    public Image blackImage;
    public BombScript2 bs2;

    void Update()
    {
        if (bs2.demoFinished == true)
        {
            StartCoroutine(FadeToBlack());

        }
    }
    public IEnumerator FadeToBlack()
    {
        float fadeDuration = 1.5f;
        float startAlpha = blackImage.color.a;
        float targetAlpha = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            Color newColor = blackImage.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            blackImage.color = newColor;

            yield return null;
        }

        Color finalColor = blackImage.color;
        finalColor.a = targetAlpha;
        blackImage.color = finalColor;
        SceneManager.LoadScene("FinalScene");
    }
}
