
using UnityEngine;

public class MonsterVision : MonoBehaviour
{
    [Header("Ziel")]
    public Transform player;
    public Transform eyes;
    public MonsterAI monsterAI;

    [Header("Sicht")]
    public float viewDistance = 18f;
    public float viewAngle = 100f;
    public float loseSightDelay = 3f;

    [Header("Hindernisse")]
    public LayerMask obstacleMask = ~0;

    private bool playerVisible;
    private float lostSightTimer;

    private void Awake()
    {
        if (monsterAI == null)
            monsterAI = GetComponent<MonsterAI>();

        if (eyes == null)
            eyes = transform;
    }

    private void Update()
    {
        if (player == null || monsterAI == null)
            return;

        if (CanSeePlayer())
        {
            lostSightTimer = 0f;

            if (!playerVisible)
                monsterAI.PlayerDetected();

            playerVisible = true;
        }
        else if (playerVisible)
        {
            lostSightTimer += Time.deltaTime;

            if (lostSightTimer >= loseSightDelay)
            {
                playerVisible = false;
                monsterAI.LostPlayer();
            }
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 direction = player.position - eyes.position;

        if (direction.magnitude > viewDistance)
            return false;

        float angle = Vector3.Angle(
            eyes.forward,
            direction.normalized
        );

        if (angle > viewAngle / 2f)
            return false;

        if (Physics.Raycast(
            eyes.position,
            direction.normalized,
            out RaycastHit hit,
            viewDistance,
            obstacleMask))
        {
            return hit.transform == player ||
                   hit.transform.IsChildOf(player);
        }

        return false;
    }
}
