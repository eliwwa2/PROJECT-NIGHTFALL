
using UnityEngine;
using UnityEngine.AI;

public class MonsterTeleportEvent : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;

    public float minDistance = 10f;
    public float maxDistance = 18f;
    public float eventInterval = 45f;

    private float timer;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player == null || agent == null)
            return;

        timer += Time.deltaTime;

        if (timer >= eventInterval)
        {
            timer = 0f;
            TryTeleport();
        }
    }

    private void TryTeleport()
    {
        Vector3 direction = Random.insideUnitSphere;
        direction.y = 0f;
        direction.Normalize();

        float distance = Random.Range(
            minDistance, maxDistance);

        Vector3 target = player.position + direction * distance;

        if (NavMesh.SamplePosition(
            target, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            if (!IsVisibleToPlayer(hit.position))
                agent.Warp(hit.position);
        }
    }

    private bool IsVisibleToPlayer(Vector3 position)
    {
        Vector3 direction = position - player.position;

        if (Vector3.Angle(
            player.forward, direction) > 60f)
            return false;

        return !Physics.Linecast(
            player.position + Vector3.up * 1.5f,
            position + Vector3.up,
            out RaycastHit hit
        );
    }
}
