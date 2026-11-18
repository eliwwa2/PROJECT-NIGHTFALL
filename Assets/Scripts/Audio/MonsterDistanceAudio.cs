
using UnityEngine;

public class MonsterDistanceAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform player;
    public Transform monster;

    public float maxDistance = 25f;
    public float minVolume = 0f;
    public float maxVolume = 1f;
    public float transitionSpeed = 3f;

    private void Update()
    {
        if (audioSource == null ||
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

        audioSource.volume = Mathf.MoveTowards(
            audioSource.volume,
            targetVolume,
            transitionSpeed * Time.deltaTime
        );
    }
}
