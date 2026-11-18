
using UnityEngine;

public class MonsterHearing : MonoBehaviour
{
    public MonsterAI monsterAI;
    public float hearingMultiplier = 1f;

    private void OnEnable()
    {
        NoiseSystem.OnNoiseHeard += OnNoiseHeard;
    }

    private void OnDisable()
    {
        NoiseSystem.OnNoiseHeard -= OnNoiseHeard;
    }

    private void Awake()
    {
        if (monsterAI == null)
            monsterAI = GetComponent<MonsterAI>();
    }

    private void OnNoiseHeard(Vector3 position, float radius)
    {
        if (monsterAI == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            position
        );

        if (distance > radius * hearingMultiplier)
            return;

        monsterAI.HearNoise(position);
    }
}
