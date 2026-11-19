
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Search,
        Attack
    }

    public State state = State.Patrol;

    public NavMeshAgent agent;
    public Transform player;
    public Transform[] patrolPoints;

    public float patrolSpeed = 1.8f;
    public float chaseSpeed = 4.5f;
    public float searchSpeed = 2.5f;
    public float searchDuration = 8f;

    private int patrolIndex;
    private float searchTimer;
    private Vector3 lastKnownPosition;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (agent != null && agent.enabled && agent.isOnNavMesh &&
    patrolPoints != null && patrolPoints.Length > 0)
    }

    private void Update()
    {
        if (agent == null || !agent.isOnNavMesh)
            return;

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Search:
                Search();
                break;

            case State.Attack:
                agent.isStopped = true;
                break;
        }
    }

    private void Patrol()
    {
        agent.isStopped = false;
        agent.speed = patrolSpeed;

        if (patrolPoints.Length == 0)
            return;

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            SetPatrolDestination();
        }
    }

    private void SetPatrolDestination()
    {
        if (patrolPoints[patrolIndex] != null)
            agent.SetDestination(patrolPoints[patrolIndex].position);
    }

    private void Chase()
    {
        if (player == null)
            return;

        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    private void Search()
    {
        agent.isStopped = false;
        agent.speed = searchSpeed;

        searchTimer += Time.deltaTime;

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            if (searchTimer >= searchDuration)
                ReturnToPatrol();
        }
        else if (searchTimer >= searchDuration * 2f)
        {
            ReturnToPatrol();
        }
    }

    private void ReturnToPatrol()
    {
        state = State.Patrol;

        if (patrolPoints.Length > 0)
            SetPatrolDestination();
    }

    public void HearNoise(Vector3 noisePosition)
    {
        if (state == State.Chase || state == State.Attack)
            return;
            if (agent == null || !agent.enabled || !agent.isOnNavMesh)
    return;

        lastKnownPosition = noisePosition;
        searchTimer = 0f;
        state = State.Search;

        agent.isStopped = false;
        agent.SetDestination(lastKnownPosition);
    }

    public void PlayerDetected()
    {
        if (player == null)
            return;
            if (agent == null || !agent.enabled || !agent.isOnNavMesh)
    return;

        state = State.Chase;
    }

    public void LostPlayer()
    {
        if (player == null)
            return;

        lastKnownPosition = player.position;
        searchTimer = 0f;
        state = State.Search;

        agent.isStopped = false;
        agent.SetDestination(lastKnownPosition);
    }
}
