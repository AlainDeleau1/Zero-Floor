using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera playerCam;
    public PauseMenu pm;
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 OriginalPosCam = playerCam.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration && pm.paused == false)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, OriginalPosCam.z);

            elapsed += Time.deltaTime;
            
            yield return null;
        }

        playerCam.transform.localPosition = OriginalPosCam;
    }
}
