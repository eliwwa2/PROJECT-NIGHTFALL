
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileSprint : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    public MobilePlayerMovement movement;
    public float sprintSpeed = 5f;
    public float normalSpeed = 2.5f;

    private void Start()
    {
        if (movement != null)
            movement.walkSpeed = normalSpeed;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (movement != null)
            movement.walkSpeed = sprintSpeed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (movement != null)
            movement.walkSpeed = normalSpeed;
    }

    private void OnDisable()
    {
        if (movement != null)
            movement.walkSpeed = normalSpeed;
    }
}
