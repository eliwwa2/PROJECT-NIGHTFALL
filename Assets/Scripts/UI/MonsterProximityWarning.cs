
using UnityEngine;
using UnityEngine.UI;

public class MonsterProximityWarning : MonoBehaviour
{
    public Transform player;
    public Transform monster;
    public Image warningOverlay;

    public float warningDistance = 12f;
    public float pulseSpeed = 4f;
    public float maxAlpha = 0.35f;

    private void Update()
    {
        if (player == null || monster == null ||
            warningOverlay == null)
            return;

        float distance = Vector3.Distance(
            player.position,
            monster.position
        );

        float danger = 1f - Mathf.Clamp01(
            distance / warningDistance
        );

        float pulse = (Mathf.Sin(
            Time.time * pulseSpeed) + 1f) / 2f;

        Color color = warningOverlay.color;

        color.a = danger * pulse * maxAlpha;

        warningOverlay.color = color;
    }
}
