
using UnityEngine;

public class HorrorLightFlicker : MonoBehaviour
{
    public Light targetLight;
    public Transform player;
    public Transform monster;

    public float dangerDistance = 15f;
    public float normalIntensity = 1f;
    public float flickerSpeed = 12f;
    public float flickerAmount = 0.8f;

    private void Update()
    {
        if (targetLight == null ||
            player == null || monster == null)
            return;

        float distance = Vector3.Distance(
            player.position,
            monster.position
        );

        if (distance > dangerDistance)
        {
            targetLight.intensity = normalIntensity;
            return;
        }

        float danger = 1f - Mathf.Clamp01(
            distance / dangerDistance
        );

        float flicker = Mathf.PerlinNoise(
            Time.time * flickerSpeed, 0f
        );

        targetLight.intensity = Mathf.Lerp(
            normalIntensity,
            normalIntensity * (1f - flickerAmount),
            flicker * danger
        );
    }
}
