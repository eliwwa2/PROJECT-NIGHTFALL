
using UnityEngine;
using UnityEngine.AI;

public class MonsterTrail : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject trailPrefab;

    public float spawnInterval = 5f;
    public float minimumMovement = 2f;

    private float timer;
    private Vector3 lastPosition;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        lastPosition = transform.position;
    }

    private void Update()
    {
        if (agent == null || trailPrefab == null)
            return;

        if (Vector3.Distance(
            transform.position, lastPosition) < minimumMovement)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTrail();
            timer = 0f;
        }
    }

    private void SpawnTrail()
    {
        Instantiate(
            trailPrefab,
            transform.position,
            Quaternion.identity
        );

        lastPosition = transform.position;
    }
}
