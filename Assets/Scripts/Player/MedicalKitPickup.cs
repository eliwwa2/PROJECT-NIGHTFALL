
using UnityEngine;

public class MedicalKitPickup : MonoBehaviour
{
    public float healAmount = 35f;

    private bool collected;

    public void Collect(PlayerHealth playerHealth)
    {
        if (collected || playerHealth == null)
            return;

        if (playerHealth.IsDead)
            return;

        if (playerHealth.currentHealth >= playerHealth.maxHealth)
            return;

        playerHealth.Heal(healAmount);

        collected = true;
        gameObject.SetActive(false);

        Debug.Log("Medikit eingesammelt! Du wurdest geheilt.");
    }
}
