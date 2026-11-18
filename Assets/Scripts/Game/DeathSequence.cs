
using System.Collections;
using UnityEngine;

public class DeathSequence : MonoBehaviour
{
    [Header("Bildschirm")]
    public CanvasGroup fadeOverlay;
    public GameObject gameOverPanel;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip deathSound;

    [Header("Einstellungen")]
    public float fadeDuration = 3f;
    public float gameOverDelay = 1f;

    private bool deathStarted;

    private void Start()
    {
        if (fadeOverlay != null)
            fadeOverlay.alpha = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void StartDeath()
    {
        if (deathStarted)
            return;

        deathStarted = true;
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        if (audioSource != null && deathSound != null)
            audioSource.PlayOneShot(deathSound);

        if (fadeOverlay != null)
        {
            fadeOverlay.gameObject.SetActive(true);

            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;

                fadeOverlay.alpha = Mathf.Clamp01(
                    elapsed / fadeDuration
                );

                yield return null;
            }

            fadeOverlay.alpha = 1f;
        }

        yield return new WaitForSecondsRealtime(gameOverDelay);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}
