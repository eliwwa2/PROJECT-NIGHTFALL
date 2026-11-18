
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake(float duration, float intensity)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(
            ShakeRoutine(duration, intensity)
        );
    }

    private IEnumerator ShakeRoutine(
        float duration,
        float intensity)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;

            transform.localPosition =
                originalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        shakeRoutine = null;
    }

    private void OnDisable()
    {
        transform.localPosition = originalPosition;
    }
}
