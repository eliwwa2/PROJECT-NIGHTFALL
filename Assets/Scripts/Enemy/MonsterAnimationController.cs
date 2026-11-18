
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class MonsterAnimationController : MonoBehaviour
{
    public NavMeshAgent agent;
    public MonsterAI monsterAI;
    public Animator animator;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (monsterAI == null)
            monsterAI = GetComponent<MonsterAI>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator == null || agent == null)
            return;

        float speed = agent.velocity.magnitude;

        animator.SetFloat(
            "Speed",
            speed,
            0.15f,
            Time.deltaTime
        );

        animator.SetBool(
            "IsChasing",
            monsterAI != null &&
            monsterAI.state == MonsterAI.State.Chase
        );
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDeath()
    {
        animator.SetBool("IsDead", true);
    }
}
