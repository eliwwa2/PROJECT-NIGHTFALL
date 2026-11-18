
using UnityEngine;

public class DynamicHorrorMusic : MonoBehaviour
{
    public AudioSource musicSource;
    public Transform player;
    public Transform monster;

    public float maxDistance = 30f;
    public float minVolume = 0.1f;
    public float maxVolume = 1f;
    public float transitionSpeed = 2f;

    private void Update()
    {
        if (musicSource == null ||
            player == null ||
            monster == null)
            return;

        float distance = Vector3.Distance(
            player.position,
            monster.position
        );

        float danger = 1f - Mathf.Clamp01(
            distance / maxDistance
        );

        float targetVolume = Mathf.Lerp(
            minVolume,
            maxVolume,
            danger
        );

        musicSource.volume = Mathf.MoveTowards(
            musicSource.volume,
            targetVolume,
            transitionSpeed * Time.deltaTime
        );
    }
}
