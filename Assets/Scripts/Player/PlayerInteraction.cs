
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 3f;
    public LayerMask interactionMask = ~0;

    public InventorySystem inventory;

    public void Interact()
    {
        if (playerCamera == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance,
            interactionMask))
        {
            DoorLock door = hit.collider.GetComponentInParent<DoorLock>();

            if (door != null)
            {
                door.Interact(inventory);
                return;
            }

            ItemPickup item = hit.collider.GetComponentInParent<ItemPickup>();

            if (item != null && inventory != null)
            {
                item.Collect(inventory);
                return;
            }

            MedicalKitPickup medikit =
                hit.collider.GetComponentInParent<MedicalKitPickup>();

            if (medikit != null)
            {
                PlayerHealth health =
                    GetComponent<PlayerHealth>();

                medikit.Collect(health);
            }
        }
    }
}
