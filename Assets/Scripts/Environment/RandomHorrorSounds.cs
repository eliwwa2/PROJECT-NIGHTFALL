
using UnityEngine;

public class RandomHorrorSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] horrorSounds;

    public float minInterval = 15f;
    public float maxInterval = 40f;

    private float timer;
    private float nextSoundTime;

    private void Start()
    {
        SetNextSoundTime();
    }

    private void Update()
    {
        if (audioSource == null ||
            horrorSounds == null ||
            horrorSounds.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= nextSoundTime)
        {
            PlayRandomSound();
            timer = 0f;
            SetNextSoundTime();
        }
    }

    private void PlayRandomSound()
    {
        int index = Random.Range(0, horrorSounds.Length);

        audioSource.PlayOneShot(horrorSounds[index]);

        Debug.Log("Ein unheimliches Geräusch ertönt...");
    }

    private void SetNextSoundTime()
    {
        nextSoundTime = Random.Range(
            minInterval, maxInterval);
    }
}
