
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JumpscareController : MonoBehaviour
{
    [Header("Kamera")]
    public Camera playerCamera;
    public CameraShake cameraShake;

    [Header("Jumpscare Anzeige")]
    public CanvasGroup jumpscareOverlay;
    public Image jumpscareImage;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip jumpscareSound;

    [Header("Effekte")]
    public float duration = 1.5f;
    public float shakeDuration = 0.8f;
    public float shakeIntensity = 0.12f;

    private bool isPlaying;

    public void PlayJumpscare(Transform monster)
    {
        if (isPlaying)
            return;

        if (jumpscareOverlay == null ||
            jumpscareImage == null)
            return;

        StartCoroutine(JumpscareRoutine(monster));
    }

    private IEnumerator JumpscareRoutine(Transform monster)
    {
        isPlaying = true;

        jumpscareImage.enabled = true;
        jumpscareOverlay.alpha = 1f;

        if (audioSource != null && jumpscareSound != null)
            audioSource.PlayOneShot(jumpscareSound);

        if (cameraShake != null)
            cameraShake.Shake(shakeDuration, shakeIntensity);

        if (playerCamera != null && monster != null)
        {
            Vector3 direction =
                monster.position - playerCamera.transform.position;

            if (direction.sqrMagnitude > 0.001f)
            {
                playerCamera.transform.rotation =
                    Quaternion.LookRotation(direction);
            }
        }

        yield return new WaitForSecondsRealtime(duration);

        jumpscareOverlay.alpha = 0f;
        jumpscareImage.enabled = false;

        isPlaying = false;
    }
}
