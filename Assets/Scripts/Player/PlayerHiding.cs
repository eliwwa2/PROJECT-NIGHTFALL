
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    [Header("Versteck")]
    public Transform hidePoint;
    public Transform exitPoint;

    [Header("Spieler")]
    public CharacterController characterController;
    public MonoBehaviour[] controlsToDisable;

    public bool IsHiding { get; private set; }

    private void Awake()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
    }

    public void EnterHideSpot(Transform hidingPosition, Transform exitPosition)
    {
        if (IsHiding || hidingPosition == null)
            return;

        hidePoint = hidingPosition;
        exitPoint = exitPosition;

        IsHiding = true;

        if (characterController != null)
            characterController.enabled = false;

        transform.SetPositionAndRotation(
            hidePoint.position,
            hidePoint.rotation
        );

        if (characterController != null)
            characterController.enabled = true;

        foreach (MonoBehaviour control in controlsToDisable)
        {
            if (control != null)
                control.enabled = false;
        }
    }

    public void ExitHideSpot()
    {
        if (!IsHiding)
            return;

        IsHiding = false;

        if (characterController != null)
            characterController.enabled = false;

        if (exitPoint != null)
        {
            transform.SetPositionAndRotation(
                exitPoint.position,
                exitPoint.rotation
            );
        }

        if (characterController != null)
            characterController.enabled = true;

        foreach (MonoBehaviour control in controlsToDisable)
        {
            if (control != null)
                control.enabled = true;
        }
    }
}
