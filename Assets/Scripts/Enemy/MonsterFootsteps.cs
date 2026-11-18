
using UnityEngine;
using UnityEngine.AI;

public class MonsterFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    public NavMeshAgent agent;

    public float walkInterval = 0.65f;
    public float runInterval = 0.35f;
    public float runSpeed = 3.5f;

    private float timer;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (agent == null || audioSource == null)
            return;

        if (agent.velocity.magnitude < 0.2f)
            return;

        float interval =
            agent.velocity.magnitude >= runSpeed
            ? runInterval
            : walkInterval;

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            PlayFootstep();
        }
    }

    private void PlayFootstep()
    {
        if (footstepSounds == null ||
            footstepSounds.Length == 0)
            return;

        AudioClip sound = footstepSounds[
            Random.Range(0, footstepSounds.Length)];

        audioSource.PlayOneShot(sound);
    }
}
