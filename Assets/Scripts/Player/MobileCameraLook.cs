
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileCameraLook : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler
{
    public Transform playerBody;
    public Transform playerCamera;

    public float sensitivity = 0.15f;
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;

    private float verticalRotation;

    private void Start()
    {
        if (playerCamera != null)
            verticalRotation = playerCamera.localEulerAngles.x;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (playerBody == null || playerCamera == null)
            return;

        float mouseX = eventData.delta.x * sensitivity;
        float mouseY = eventData.delta.y * sensitivity;

        playerBody.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(
            verticalRotation,
            minVerticalAngle,
            maxVerticalAngle
        );

        playerCamera.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
