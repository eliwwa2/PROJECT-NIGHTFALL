
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterSearchHiding : MonoBehaviour
{
    [Header("Monster")]
    public NavMeshAgent agent;
    public MonsterAI monsterAI;

    [Header("Suche")]
    public float searchRadius = 12f;
    public float checkDuration = 3f;
    public float detectionChance = 0.35f;

    private bool isChecking;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (monsterAI == null)
            monsterAI = GetComponent<MonsterAI>();
    }

    public void CheckHideSpot(HideSpot spot)
    {
        if (spot == null || isChecking)
            return;

        StartCoroutine(CheckRoutine(spot));
    }

    private IEnumerator CheckRoutine(HideSpot spot)
    {
        isChecking = true;

        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(spot.transform.position);

            while (agent.pathPending ||
                   agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(checkDuration);

        PlayerHiding hiding = FindFirstObjectByType<PlayerHiding>();

        if (hiding != null &&
            hiding.IsHiding &&
            Random.value < detectionChance)
        {
            hiding.ExitHideSpot();

            if (monsterAI != null)
                monsterAI.PlayerDetected();
        }

        isChecking = false;
    }
}
