
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    public void PlayOpenSound()
    {
        if (audioSource != null && openSound != null)
            audioSource.PlayOneShot(openSound);

        NoiseSystem.EmitNoise(transform.position, 12f);
    }

    public void PlayCloseSound()
    {
        if (audioSource != null && closeSound != null)
            audioSource.PlayOneShot(closeSound);

        NoiseSystem.EmitNoise(transform.position, 10f);
    }
}
