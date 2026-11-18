
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerNoise : MonoBehaviour
{
    public float walkNoiseRadius = 8f;
    public float runNoiseRadius = 17f;
    public float runSpeedThreshold = 3.2f;
    public float noiseInterval = 0.5f;

    private CharacterController controller;
    private float timer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!controller.isGrounded)
            return;

        float speed = controller.velocity.magnitude;

        if (speed < 0.2f)
            return;

        timer += Time.deltaTime;

        if (timer < noiseInterval)
            return;

        timer = 0f;

        float radius = speed >= runSpeedThreshold
            ? runNoiseRadius
            : walkNoiseRadius;

        NoiseSystem.EmitNoise(transform.position, radius);
    }
}
