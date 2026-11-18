
using System.Collections;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [Header("Tür")]
    public Transform door;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    [Header("Schloss")]
    public bool locked = true;
    public InventorySystem.ItemType requiredKey =
        InventorySystem.ItemType.HospitalKey;

    private bool isOpen;
    private bool isMoving;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Awake()
    {
        if (door == null)
            door = transform;

        closedRotation = door.localRotation;

        openRotation = closedRotation *
            Quaternion.Euler(0f, openAngle, 0f);
    }

    public void Interact(InventorySystem inventory)
    {
        if (isMoving)
            return;

        if (locked && !isOpen)
        {
            if (inventory == null ||
                !inventory.HasItem(requiredKey))
            {
                Debug.Log("Du benötigst den passenden Schlüssel!");
                return;
            }

            inventory.RemoveItem(requiredKey);
            locked = false;
        }

        StartCoroutine(ToggleDoor());
    }

    private IEnumerator ToggleDoor()
    {
        isMoving = true;

        Quaternion targetRotation =
            isOpen ? closedRotation : openRotation;

        while (Quaternion.Angle(
            door.localRotation,
            targetRotation) > 0.5f)
        {
            door.localRotation = Quaternion.Slerp(
                door.localRotation,
                targetRotation,
                Time.deltaTime * openSpeed
            );

            yield return null;
        }

        door.localRotation = targetRotation;
        isOpen = !isOpen;
        isMoving = false;
    }
}
