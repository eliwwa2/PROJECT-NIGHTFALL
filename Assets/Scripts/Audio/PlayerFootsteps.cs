
using UnityEngine;
using UnityEngine.CharacterController;

public class PlayerFootsteps : MonoBehaviour
{
    public CharacterController controller;
    public AudioSource audioSource;

    public AudioClip[] walkSounds;
    public AudioClip[] runSounds;

    public float walkInterval = 0.55f;
    public float runInterval = 0.3f;
    public float runSpeed = 3.5f;

    private float timer;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (controller == null || audioSource == null)
            return;

        float speed = controller.velocity.magnitude;

        if (!controller.isGrounded || speed < 0.2f)
            return;

        bool running = speed >= runSpeed;

        timer += Time.deltaTime;

        float interval = running ? runInterval : walkInterval;

        if (timer >= interval)
        {
            timer = 0f;
            PlayFootstep(running);
        }
    }

    private void PlayFootstep(bool running)
    {
        AudioClip[] sounds = running ? runSounds : walkSounds;

        if (sounds == null || sounds.Length == 0)
            return;

        AudioClip sound = sounds[
            Random.Range(0, sounds.Length)];

        audioSource.PlayOneShot(sound);
    }
}
