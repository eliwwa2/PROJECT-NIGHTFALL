
using UnityEngine;

public class MonsterTracking : MonoBehaviour
{
    public MonsterAI monsterAI;
    public Transform player;

    public float trackingDistance = 20f;
    public float updateInterval = 2f;

    private float timer;
    private Vector3 lastPosition;

    private void Awake()
    {
        if (monsterAI == null)
            monsterAI = GetComponent<MonsterAI>();
    }

    private void Update()
    {
        if (player == null || monsterAI == null)
            return;

        timer += Time.deltaTime;

        if (timer < updateInterval)
            return;

        timer = 0f;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= trackingDistance &&
            monsterAI.state != MonsterAI.State.Chase)
        {
            lastPosition = player.position;
            monsterAI.HearNoise(lastPosition);
        }
    }
}
