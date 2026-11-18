
using UnityEngine;
using UnityEngine.AI;

public class MonsterRotation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public float rotationSpeed = 5f;
    public float movementThreshold = 0.1f;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent != null)
            agent.updateRotation = false;
    }

    private void Update()
    {
        if (agent == null)
            return;

        Vector3 direction = Vector3.zero;

        if (agent.velocity.magnitude > movementThreshold)
        {
            direction = agent.velocity;
        }
        else if (player != null)
        {
            direction = player.position - transform.position;
        }

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
