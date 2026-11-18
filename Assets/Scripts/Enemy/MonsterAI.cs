
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

    [Header("Monster")]
    public State state = State.Patrol;
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Geschwindigkeit")]
    public float patrolSpeed = 1.8f;
    public float chaseSpeed = 4.5f;
    public float searchSpeed = 2.5f;

    [Header("Suche")]
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
        if (agent != null && patrolPoints.Length > 0)
            SetPatrolDestination();
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
        }
    }

    private void Patrol()
    {
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

        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    private void Search()
    {
        agent.speed = searchSpeed;
        searchTimer += Time.deltaTime;

        if (searchTimer >= searchDuration)
        {
            state = State.Patrol;

            if (patrolPoints.Length > 0)
                SetPatrolDestination();
        }
    }

    public void HearNoise(Vector3 noisePosition)
    {
        if (state == State.Chase || state == State.Attack)
            return;

        lastKnownPosition = noisePosition;
        searchTimer = 0f;
        state = State.Search;

        if (agent != null && agent.isOnNavMesh)
        {
            agent.speed = searchSpeed;
            agent.SetDestination(lastKnownPosition);
        }

        Debug.Log("Monster hat ein Geräusch gehört!");
    }

    public void PlayerDetected()
    {
        if (player == null)
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

        if (agent != null && agent.isOnNavMesh)
            agent.SetDestination(lastKnownPosition);
    }
}
