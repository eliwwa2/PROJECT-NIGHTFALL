
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MobilePlayerMovement : MonoBehaviour
{
    public MobileJoystick joystick;
    public Transform cameraTransform;

    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float gravity = -20f;

    private CharacterController controller;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (joystick == null || cameraTransform == null)
            return;

        Vector2 input = joystick.InputDirection;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement =
            forward * input.y + right * input.x;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity =
            movement * walkSpeed +
            Vector3.up * verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}
