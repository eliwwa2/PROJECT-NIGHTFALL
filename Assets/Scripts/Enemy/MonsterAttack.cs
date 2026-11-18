
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float damage = 25f;
    public float attackRange = 1.8f;
    public float attackCooldown = 1.5f;

    public Transform player;
    public MonsterAI monsterAI;
    public Animator animator;

    private float nextAttackTime;

    private void Awake()
    {
        if (monsterAI == null)
            monsterAI = GetComponent<MonsterAI>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null || monsterAI == null)
            return;

        if (monsterAI.state != MonsterAI.State.Chase &&
            monsterAI.state != MonsterAI.State.Attack)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance > attackRange)
        {
            if (monsterAI.state == MonsterAI.State.Attack)
                monsterAI.state = MonsterAI.State.Chase;

            return;
        }

        monsterAI.state = MonsterAI.State.Attack;

        if (Time.time >= nextAttackTime)
            TryAttack();
    }

    private void TryAttack()
    {
        nextAttackTime = Time.time + attackCooldown;

        if (animator != null)
            animator.SetTrigger("Attack");

        PlayerHealth health = player.GetComponent<PlayerHealth>();

        if (health != null)
            health.TakeDamage(damage);
    }
}
