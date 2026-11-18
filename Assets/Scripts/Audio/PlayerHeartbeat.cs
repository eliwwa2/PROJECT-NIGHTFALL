
using UnityEngine;

public class PlayerHeartbeat : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip heartbeatSound;

    public Transform player;
    public Transform monster;

    public float maxDistance = 25f;
    public float slowInterval = 2f;
    public float fastInterval = 0.45f;

    private float timer;

    private void Update()
    {
        if (player == null || monster == null ||
            audioSource == null || heartbeatSound == null)
            return;

        float distance = Vector3.Distance(
            player.position,
            monster.position
        );

        if (distance > maxDistance)
            return;

        float danger = 1f - Mathf.Clamp01(
            distance / maxDistance
        );

        float interval = Mathf.Lerp(
            slowInterval,
            fastInterval,
            danger
        );

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            audioSource.PlayOneShot(heartbeatSound);
            timer = 0f;
        }
    }
}
